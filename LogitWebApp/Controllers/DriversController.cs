using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.Services.Drivers;
using LogitWebApp.ViewModels.Drivers;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriversService driversService;

        public DriversController(IDriversService driversService)
        {
            this.driversService = driversService;
        }


        public IActionResult Register()
        {
            //Само Admin може да извиква този action

            return View();
        }

        [HttpPost]
        public IActionResult Register(DriverInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            bool isDriverExist = this.driversService.IsDriverExist(input);

            if (isDriverExist)
            {
                return this.Redirect("DriverExist");
            }

            this.driversService.AddDriver(input);

            return this.Redirect("DriverAdded");
        }

        public IActionResult DriverAdded()
        {
            return this.View();
        }
        public IActionResult DriverExist()
        {
            return this.View();
        }
    }
}
