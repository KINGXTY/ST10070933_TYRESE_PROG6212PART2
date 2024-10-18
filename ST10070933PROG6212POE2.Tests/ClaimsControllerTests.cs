using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ST10070933PROG6212POE2.Controllers;
using ST10070933PROG6212POE2.Data;
using ST10070933PROG6212POE2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit; 

public class ClaimsControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IWebHostEnvironment> _mockEnvironment;
    private readonly Mock<ILogger<ClaimsController>> _mockLogger;
    private readonly ClaimsController _controller;

    public ClaimsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use unique DB for each test
            .Options;

        _context = new ApplicationDbContext(options);
        _mockEnvironment = new Mock<IWebHostEnvironment>();
        _mockLogger = new Mock<ILogger<ClaimsController>>();

        _controller = new ClaimsController(_context, _mockEnvironment.Object, _mockLogger.Object);
    }

    [Fact] 
    public async Task Submit_ValidClaim_ShouldSaveToDatabase()
    {
        var model = new ClaimViewModel
        {
            LecturerId = 1,
            HoursWorked = 10,
            HourlyRate = 20,
            Notes = "Test submission"
        };

        var files = new List<IFormFile>(); 

        var result = await _controller.Submit(model, files);

        Assert.True(_controller.ModelState.IsValid, "ModelState is invalid.");
        var claim = await _context.Claims.FirstOrDefaultAsync();
        Assert.NotNull(claim); 
        Assert.IsType<RedirectToActionResult>(result);
    }
}