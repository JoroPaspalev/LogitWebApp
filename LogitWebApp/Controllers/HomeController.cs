using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LogitWebApp.Models;
using LogitWebApp.ViewModels.Offer;
using LogitWebApp.Services.Home;
using LogitWebApp.Views.Home;
using Microsoft.AspNetCore.Authorization;

namespace LogitWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService homeService;

        public HomeController(
            ILogger<HomeController> logger, 
            IHomeService homeService)
        {
            _logger = logger;
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            var model = new OfferInputModel();
            model.Cities = this.homeService.GetCities();
            return View(model);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(OfferInputModel input)
        {
            if (!ModelState.IsValid)
            {
                input.Cities = this.homeService.GetCities();
                return this.View(input);
            }

            if (input.From == input.To)
            {
                return this.Redirect($"/Offers/ShipmentInSameCity?cityName={input.From}");
            }

            string shipmentId = this.homeService.CreateShipment(input);

            return this.RedirectToAction("Calculate", "Offers", new { ShipmentId = shipmentId });
        }

        public IActionResult Exception()
        {
            throw new Exception("Error message by JP");
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

        public IActionResult StatusCodeError(int errorCode)
        {
            if (errorCode == 404)
            {
                return this.View("404");
            }
            else if (errorCode == 500)
            {
                //За да се зареди това View не е нужно да пишем "/Home/500" защото автоматично се търси в папка /Home подаденото име на View - в случая 500. Ако там го няма го търси в Views/Shared
                return this.View("500");
            }
            else
            {
                return this.View(errorCode);
            }
        }

        public IActionResult MyTestView()
        {
            var viewModel = new OfferInputModel
            {
                Weight = 12,
                Height = 122,
                Length = 234,
                IsFragile = true,
                Width = 99,
                CountOfPallets = 33
            };

            var model = new TestViewModel()
            {
                CountOfPallets = 234,
                CategoryId = 3
            };

            return this.View(model);
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }
    }
}
