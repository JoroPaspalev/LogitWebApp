using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LogitWebApp.Models;
using LogitWebApp.Services.SeedDb;
using LogitWebApp.ViewModels.Offer;

namespace LogitWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ISeedService seedService)
        {
            _logger = logger;
            //Start only first time to seed data in Db
            //seedService.SeedDb();
        }

        public IActionResult Index()
        {
            var isAuthenticated = this.HttpContext.User.Identity.IsAuthenticated;

          
            return View();
        }

        public IActionResult Exception()
        {
            throw new Exception("Error message by JP");
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


        public IActionResult MyErrorAction()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult MyTestView()
        {
            var viewModel = new OfferInputModel
            {
                Weight = 12,
                Height = 122,
                Length = 234,
                IsFragile = true.ToString(),
                Width = 99,
                CountOfPallets = 33
            };

            return this.View(viewModel);
        }
    }
}
