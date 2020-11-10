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
                this.ViewBag.ShipmentId = input.ShipmentId;

                return this.View();
            }

            this.ordersService.CreateOrder(input);

            ViewBag.ShipmentId = input.ShipmentId;//Това да го махна???

            return this.RedirectToAction("OrderAdded", "Orders", new { Shipmentid = input.ShipmentId});
        }


        public IActionResult OrderAdded(string shipmentId)
        {
            ViewBag.ShipmentId = shipmentId;

            return this.View();
        }
    }
}
