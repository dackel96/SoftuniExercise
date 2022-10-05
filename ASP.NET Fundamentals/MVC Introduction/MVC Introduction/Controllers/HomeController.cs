using Microsoft.AspNetCore.Mvc;
using MVC_Introduction.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MVC_Introduction.Controllers
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
            ViewBag.Message = "I,m message from ViewBag";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "some text about this page!";
            return View();
        }

        public IActionResult Numbers()
        {

            return View();
        }

        public IActionResult NumbersToN(int count = 3)
        {
            ViewBag.Count = count;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}