using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LogitWebApp.Services.Drivers;
using LogitWebApp.ViewModels.Drivers;
using LogitWebApp.ViewModels.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriversService driversService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DriversController(IDriversService driversService, IWebHostEnvironment webHostEnvironment)
        {
            this.driversService = driversService;
            this.webHostEnvironment = webHostEnvironment;
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

            return this.RedirectToAction("DriverAdded", "Drivers", new ChangesApplied
            {
                Message = "Вие успешно добавихте шофьор към базата данни!"
            });
        }

        public IActionResult DriverAdded(ChangesApplied input)
        {
            return this.View(input);
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



        public IActionResult ShipmentChanged(ChangesApplied input)
        {
            return this.View(input);
        }

        [HttpPost]
        public IActionResult Edit(string shipmentId)
        {
            return RedirectToAction("EditShipment", "Drivers", new IdOfTheShipment { ShipmentId = shipmentId });
        }


        public IActionResult EditShipment(IdOfTheShipment idOfTheShipment)
        {
            //да взема данните на тази пратка по Id-то да направя viewModel и да го подам на view-то

            var shipmentForEdit = this.driversService.GetShipment(idOfTheShipment.ShipmentId);

            return this.View(shipmentForEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditShipment(EditShipment input)
        {
            //if (input.Picture != null)
            //{
            //    if (!input.Picture.FileName.EndsWith(".png") && !input.Picture.FileName.EndsWith(".jpg"))
            //    {
            //        //Така добавяме грешки в Controller-а
            //        this.ModelState.AddModelError("Picture", "Можете да качвате само файлове с разширение .png и .jpg!"); 
            //    }
            //}

            //if (input.Picture != null)
            //{
            //    if (input.Picture.Length > 2 * 1024 * 1024)
            //    {
            //        this.ModelState.AddModelError("Picture", $"Можете да качвате файлове до 2MB!");
            //    }
            //}


            if (!ModelState.IsValid)
            {
                var shipmentForEdit = this.driversService.GetShipment(input.ShipmentId);

                return this.View(shipmentForEdit);
            }

            if (input.Picture != null)
            {
                //Ако файла мине през проверките го записваме 
                var currDate = DateTime.UtcNow.ToString("yyyy_MM_ddTHH_mm_ss", CultureInfo.InvariantCulture);

                var index = input.Picture.FileName.LastIndexOf('.');
                var extension = input.Picture.FileName.Substring(index);

                //Отвори ми файлов стрийм към wwwroot/proof/име на файла в режим на създаване на нов файл и вземи данните от Picture и ми ги копирай в посочения stream
                using (FileStream fileStream = new FileStream(this.webHostEnvironment.WebRootPath + "/proof/" + input.ShipmentId + "__" + currDate + "." + extension, FileMode.Create))
                {
                    await input.Picture.CopyToAsync(fileStream);
                }
            }

            //TODO - chain .jpg file with shipmentId
            
            this.driversService.ChangeShipmentData(input);

            return this.RedirectToAction("ShipmentChanged", "Drivers", new ChangesApplied
            {
                Message = "Вие успешно променихте поръчка с номер:",
                ShipmentId = input.ShipmentId
            });
        }


    }
}
