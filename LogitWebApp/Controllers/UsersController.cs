using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Users;
using LogitWebApp.ViewModels.Pagination;
using LogitWebApp.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult AllUserOrders(int id = 1)
        {
            //Ще се връща View с всички поръчки на дадения потребител като последната ще е най-отгоре
            var userId = this.userManager.GetUserId(User);

            PaginationViewModel allShipments = this.usersService.GetAllUserOrders(userId, id, 1);

            return View(allShipments);

            //return this.PhysicalFile(this.webHostEnvironment.WebRootPath + "/proof/1.jpg", "image/jpg");
            //Това генерира това
            //<img style="-webkit-user-select: none;margin: auto;cursor: zoom-in;" src="https://localhost:44314/users/AllUserShipments" width="524" height="272">
        }


        public IActionResult ShowImages(string orderId)
        {
            UserOrderViewModel model = this.usersService.GetOrder(orderId);

            return this.View(model);
        }


    }
}
