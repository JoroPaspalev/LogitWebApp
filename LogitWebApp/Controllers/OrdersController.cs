using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Orders;
using LogitWebApp.ViewModels.Offer;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult Create(string shipmentId)
        {
            this.ViewBag.ShipmentId = shipmentId;

            return this.View();//Ще се връща View с всички поръчки на дадения потребител като последната ще е най-отгоре
        }

        [HttpPost]
        public IActionResult Create(AddressInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            this.ordersService.CreateOrder(input);

            return this.Redirect("/");
        }
    }
}
