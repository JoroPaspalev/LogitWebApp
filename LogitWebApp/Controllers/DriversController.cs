using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public IActionResult Shipments()
        {
            return this.View();
        }

        public IActionResult All()
        {
            var allShipments = this.driversService.GetAllShipments();

            return this.View(allShipments);
        }

        public IActionResult TakeShipment(string shipmentId)
        {

            //Няма още такова View 
            //Трябва да намеря пратката по Id и да и закача Id-то на логнатия Driver
            return this.View();
        }

        public IActionResult MyShipments()
        {
            //Вземи ми всички пратки по Id на този шофьор и ми ги покажи

            //вземам му Id-то
            //var driverId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string driverId = null;

            var driverShipments = this.driversService.GetMyShipments(driverId);

            return this.View(driverShipments);
        }

        [HttpPost]
        public IActionResult MyShipments( EditShipment input)
        {
            if (!ModelState.IsValid)
            {
                //Вземи ми всички пратки по Id на този шофьор и ми ги покажи
                //вземам му Id-то
                //var driverId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string driverId = null;

                var driverShipments = this.driversService.GetMyShipments(driverId);

                return this.View(driverShipments);                
            }

            this.driversService.ChangeShipmentData(input);

            return this.RedirectToAction("ShipmentChanged", "Drivers", new { Shipmentid = input.ShipmentId });
        }

        public IActionResult ShipmentChanged(string shipmentId)
        {
            ViewBag.ShipmentId = shipmentId;

            return this.View();
        }

    }
}
