using System;
using LogitWebApp.ViewModels.Test;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Add()
        {
            var model_With_initial_data = new AddRecepieInputModel
            {
                Name = "Musaca",
                CookingTime = 2,
                Description = "Put pattatoes to boil, after put meat and mix ",
                IsDelicious = true,
                FirstCooked = DateTime.UtcNow,
                Minutes = 2.45,
                Type = ViewModels.Test.Enums.Type.FastFood,
                Dog = new Dog
                {
                    Name = "Sharo",
                    Age =2,
                    IsDezinfeced = true                    
                }
            };

            return this.View(model_With_initial_data);
        }

        [HttpPost]
        public IActionResult Add(AddRecepieInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            //Save Data i Db
            return this.RedirectToAction("ThankYou", "Test");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        public IActionResult Shipment()
        {
            return this.View();
        }
       
        public IActionResult Index1()
        {
            return this.View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index1(string info)
        {
            ViewBag.Info = info;

            return this.View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TestInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            return this.View();
        }
    }
}


