using Microsoft.AspNetCore.Mvc;
using ST10070933PROG6212POE2.Models;
using System.Diagnostics;

namespace ST10070933PROG6212POE2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }       

        public IActionResult Review()
        {
            return View("~/Views/Claims/Review.cshtml");
        }

        public IActionResult Submit()
        {          
            return View("~/Views/Claims/Submit.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
