using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandPass.Models;
using Microsoft.AspNetCore.Http;

namespace RandPass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 1);
            }
            int? count = HttpContext.Session.GetInt32("Count");
            ViewBag.Count = count;
            // **********
            string Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string NewString = "";
            Random rand = new Random();
            rand.Next(0, Code.Length);
            for (var i = 0; i < 15; i++)
            {
                NewString += Code[rand.Next(0, Code.Length)];
            }
            ViewBag.NewString = NewString;
            return View();
        }

        [HttpGet("increment")]
        public IActionResult Increment()
        {
            int? count = HttpContext.Session.GetInt32("Count");
            count++;
            HttpContext.Session.SetInt32("Count", (int)count);
            return RedirectToAction("Index");
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


