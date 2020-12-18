using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Drivers;
using LogitWebApp.ViewModels.Drivers;
using LogitWebApp.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static LogitWebApp.Common.GlobalConstants;

namespace LogitWebApp.Controllers
{
    [Authorize(Roles = "Admin, Driver")]
    public class DriversController : Controller
    {
        private readonly IDriversService driversService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private const int CountOfItems = 6;

        public DriversController(IDriversService driversService, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            this.driversService = driversService;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }

        //Само Admin може да извиква този action
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(DriverInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await this.driversService.AddDriverAsync(input);

            return this.RedirectToAction(nameof(DriverAdded), "Drivers", new ChangesApplied { Message = Driver_Added });
        }

        public IActionResult DeleteDriver()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult DeleteDriver(DeleteDriverInputModel input)
        {
            if (!this.driversService.IsDriverExist(input.Email))
            {
                ModelState.AddModelError("Email", "Такъв Email не съществува!");
            }

            if (!ModelState.IsValid)
            {
                return this.View();
            }

            this.driversService.DeleteDriver(input.Email);

            return this.RedirectToAction("DriverDeleted", "Drivers", new ChangesApplied { Message = Driver_Deleted });
        }

        public IActionResult DriverDeleted(ChangesApplied input)
        {
            return this.View(input);
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

        public IActionResult All(int id = 1)
        {
            var model = this.driversService.GetAllShipments(id, CountOfItems);

            return this.View(model);
        }

        public async Task<IActionResult> TakeShipment(string shipmentId)
        {
            var currUser = await this.userManager.GetUserAsync(User);
            await this.driversService.AttachShipmentToDriverAsync(shipmentId, currUser.Id);

            return this.RedirectToAction("All");
        }

        public IActionResult MyShipments(int id = 1)
        {            
            var driverId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var driverShipments = this.driversService.GetMyShipments(driverId, id, 2);

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
            var shipmentForEdit = this.driversService.GetShipment(idOfTheShipment.ShipmentId);
            return this.View(shipmentForEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditShipment(EditShipment input)
        {
            if (!ModelState.IsValid)
            {
                var shipmentForEdit = this.driversService.GetShipment(input.ShipmentId);

                return this.View(shipmentForEdit);
            }

            if (input.Pictures != null)
            {
                foreach (var picture in input.Pictures)
                {
                    var index = picture.FileName.LastIndexOf('.');
                    var extension = picture.FileName.Substring(index);
                    var currDriver = await this.userManager.GetUserAsync(User);

                    var currImage = new Image
                    {
                        Extension = extension,
                        ShipmentId = input.ShipmentId,
                        AddedByDriverId = currDriver.Id,
                        CreatedOn = DateTime.UtcNow,
                    };

                    //Отвори ми файлов стрийм към wwwroot/proof/име на файла в режим на създаване на нов файл и вземи данните от Picture и ми ги копирай в посочения stream
                    string imageUrl = "/proof/" + currImage.Id + "." + extension;

                    using (FileStream fileStream = new FileStream(this.webHostEnvironment.WebRootPath + imageUrl, FileMode.Create))
                    {
                        await picture.CopyToAsync(fileStream);
                    }

                    currImage.ImageUrl = imageUrl;

                    input.Images.Add(currImage);
                }
            }

            await this.driversService.ChangeShipmentDataAsync(input);

            return this.RedirectToAction("ShipmentChanged", "Drivers", new ChangesApplied
            {
                Message = "Вие успешно променихте поръчка с номер:",
                ShipmentId = input.ShipmentId
            });
        }
    }
}
