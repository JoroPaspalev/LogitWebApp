using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.ViewModels.Test;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddRecepieInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            //Save Data i Db
            return this.RedirectToAction("/Test/ThankYou");
        }
        public IActionResult ThankYou()
        {
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


