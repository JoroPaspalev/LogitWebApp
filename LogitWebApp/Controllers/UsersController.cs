using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Users;
using LogitWebApp.ViewModels.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUsersService usersService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public UsersController(IWebHostEnvironment webHostEnvironment, IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public IActionResult AllUserShipments()
        {
            var userId = this.userManager.GetUserId(User);

            IEnumerable<UserAllShipmentsViewModel> allShipments =  this.usersService.GetAllUserShipments(userId);

            return View(allShipments);

            //return this.PhysicalFile(this.webHostEnvironment.WebRootPath + "/proof/1.jpg", "image/jpg");
            //Това генерира това
            //<img style="-webkit-user-select: none;margin: auto;cursor: zoom-in;" src="https://localhost:44314/users/AllUserShipments" width="524" height="272">



        }
    }
}
