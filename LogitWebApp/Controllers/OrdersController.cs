﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Orders;
using LogitWebApp.ViewModels.Offer;
using LogitWebApp.ViewModels.Shared;
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string orderId = this.ordersService.CreateOrder(input, userId);           

            return this.RedirectToAction("OrderAdded", "Orders", new ChangesApplied
            {
                Message = "Вие успешно добавихте поръчка с номер:",
                OrderId = orderId
            });
        }


        public IActionResult OrderAdded(ChangesApplied input)
        {
            return this.View(input);
        }
    }
}
