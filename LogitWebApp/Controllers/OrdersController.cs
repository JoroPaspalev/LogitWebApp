using System.Security.Claims;
using LogitWebApp.Common;
using LogitWebApp.Services.Orders;
using LogitWebApp.ViewModels.Offer;
using LogitWebApp.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogitWebApp.Controllers
{
    [Authorize(Roles ="Admin, User")]
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

            return this.View();
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

        //public IActionResult CreateOrder()
        //{
        //    return this.Redirect("/");
        //}

        [Authorize]
        public IActionResult Chat(string orderId)
        {
            var isAdmin = this.User.IsInRole(GlobalConstants.Admin_RoleName);
            //Ако currUser е в роля Admin то тогава отбележи, че непрочетените messages са прочетени
            if (isAdmin)
            {
                this.ordersService.ChangeAllUnreadMessagesWithThisOrderId(orderId);
            }

            var messages = this.ordersService.GetAllMessages(orderId);
            ViewBag.orderId = orderId;
            return this.View(messages);
        }

        public IActionResult OrderAdded(ChangesApplied input)
        {
            return this.View(input);
        }
    }
}
