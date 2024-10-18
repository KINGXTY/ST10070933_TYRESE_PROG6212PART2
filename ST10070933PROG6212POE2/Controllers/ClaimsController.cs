using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ST10070933PROG6212POE2.Data;
using ST10070933PROG6212POE2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ST10070933PROG6212POE2.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ClaimsController> _logger;

        public ClaimsController(
            ApplicationDbContext context,
            IWebHostEnvironment hostingEnvironment,
            ILogger<ClaimsController> logger)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Submit()
        {
            return View(new ClaimViewModel());
        }

        [HttpGet]
        public IActionResult Index()
        {
            var claims = _context.Claims.Include(c => c.Documents).ToList();
            return View(claims);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ClaimViewModel model, List<IFormFile> documents)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var claim = new Claim
                    {
                        LecturerId = model.LecturerId,
                        HoursWorked = model.HoursWorked,
                        HourlyRate = model.HourlyRate,
                        Notes = model.Notes,
                        Status = "Pending",
                        SubmissionDate = DateTime.UtcNow
                    };

                    _context.Claims.Add(claim);
                    await _context.SaveChangesAsync();

                    if (documents != null && documents.Any())
                    {
                        var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadPath);

                        foreach (var file in documents)
                        {
                            if (!IsValidFile(file))
                            {
                                throw new InvalidOperationException($"Invalid file: {file.FileName}");
                            }

                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            var filePath = Path.Combine(uploadPath, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var document = new SupportingDocument
                            {
                                ClaimId = claim.Id,
                                FileName = file.FileName,
                                FilePath = fileName,
                                ContentType = file.ContentType,
                                FileSize = file.Length,
                                UploadDate = DateTime.UtcNow
                            };

                            _context.SupportingDocuments.Add(document);
                        }

                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                 
                    _context.ChangeTracker.Clear();

                    TempData["Success"] = "Claim submitted successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error processing claim submission");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting claim for LecturerId: {LecturerId}", model.LecturerId);
                ModelState.AddModelError(string.Empty, "Claim Submitted");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Review()
        {
            var claims = _context.Claims.Include(c => c.Documents).ToList();
            return View(claims);
        }

        [HttpPost]
        public async Task<IActionResult> Review(int id, string action)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
                return NotFound();

            claim.Status = action == "approve" ? "Approved" : "Rejected";

            await _context.SaveChangesAsync();
            return Json(new { success = true, status = claim.Status });
        }

        private bool IsValidFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var allowedTypes = new[] { ".pdf", ".docx", ".xlsx" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var maxSize = 10 * 1024 * 1024; // 10MB

            return allowedTypes.Contains(extension) && file.Length <= maxSize;
        }

        // Add a method to handle file downloads if needed
        [HttpGet]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            var document = await _context.SupportingDocuments.FindAsync(id);
            if (document == null)
                return NotFound();

            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", document.FilePath);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, document.ContentType, document.FileName);
        }
    }
}