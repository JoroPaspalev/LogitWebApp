using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public UsersController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult AllUserShipments()
        {
            return View();

            //return this.PhysicalFile(this.webHostEnvironment.WebRootPath + "/proof/1.jpg", "image/jpg");
            //Това генерира това
            //<img style="-webkit-user-select: none;margin: auto;cursor: zoom-in;" src="https://localhost:44314/users/AllUserShipments" width="524" height="272">



        }
    }
}
