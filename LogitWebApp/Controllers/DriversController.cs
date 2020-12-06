using System;
using static LogitWebApp.Common.GlobalConstants;
using System.Globalization;
using System.IO;
using System.Linq;
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

            await this.driversService.AddDriver(input);

            return this.RedirectToAction(nameof(DriverAdded), "Drivers", new ChangesApplied { Message = Driver_Added });
        }

        public IActionResult DeleteDriver()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDriver(DeleteDriverInputModel input)
        {
            if (!this.driversService.IsDriverExist(input.Email))
            {
                ModelState.AddModelError("Email", "Такъв Email несъществува!");
            }

            if (!ModelState.IsValid)
            {
                return this.View();
            }

           await this.driversService.DeleteDriver(input.Email);

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
            this.driversService.AttachShipmentToDriver(shipmentId, currUser.Id);

            //Няма още такова View 
            //Трябва да намеря пратката по Id и да и закача Id-то на логнатия Driver
            return this.RedirectToAction("All");
        }

        public IActionResult MyShipments(int id = 1)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) // will give the user's userId
            //var userName = User.FindFirstValue(ClaimTypes.Name) // will give the user's userName

            //ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            //string userEmail = applicationUser?.Email; // will give the user's Email

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
                        //ImageUrl = imageUrl
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
