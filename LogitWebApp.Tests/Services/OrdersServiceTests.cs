using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Orders;
using LogitWebApp.ViewModels.Offer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogitWebApp.Tests.Services
{
    public class OrdersServiceTests
    {
        [Fact]
        public void GetAllMessagesShouldReturnExactNumberOfAllMessages()
        {
            var currOrder = new Order();
            var messages = new List<Message>()
            {
                new Message{Order = currOrder, Text = "Hello", IsRead = false, CreatedOn = DateTime.UtcNow, IsAdmin = false},
                new Message{Order = currOrder, Text = "Are you there?", IsRead = false, CreatedOn = DateTime.UtcNow, IsAdmin = false},
                new Message{Order = currOrder, Text = "Hi", IsRead = true, CreatedOn = DateTime.UtcNow, IsAdmin = false},
                new Message{Order = currOrder, Text = "I need info", IsRead = false, CreatedOn = DateTime.UtcNow, IsAdmin = false},
            };
            currOrder.Messages = messages;

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("GetAllMessages");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            db.Orders.Add(currOrder);
            db.SaveChanges();

            var ordersService = new OrdersService(db);

            var collection = ordersService.GetAllMessages(currOrder.Id);

            Assert.Equal(4, collection.Count());
        }


        [Fact]
        public void ChangeAllUnreadMessagesWithThisOrderIdShouldSetAllMessagesWithThisOrderIdToTrue()
        {
            var currOrder = new Order();
            var messages = new List<Message>()
            {
                new Message{Order = currOrder, Text = "Hello", IsRead = false, CreatedOn = DateTime.UtcNow, IsAdmin = false},
                new Message{Order = currOrder, Text = "Are you there?", IsRead = false, CreatedOn = DateTime.UtcNow, IsAdmin = false},
                new Message{Order = currOrder, Text = "Hi", IsRead = true, CreatedOn = DateTime.UtcNow, IsAdmin = false},
                new Message{Order = currOrder, Text = "I need info", IsRead = false, CreatedOn = DateTime.UtcNow, IsAdmin = false},
            };
            currOrder.Messages = messages;

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(optionsBuilder.Options);
            db.Orders.Add(currOrder);
            db.SaveChanges();

            var ordersService = new OrdersService(db);

            ordersService.ChangeAllUnreadMessagesWithThisOrderId(currOrder.Id);

            var result = db.Orders.First(o => o.Id == currOrder.Id).Messages.Where(m => m.IsRead == false).Count();

            Assert.Equal(0, result);
        }

        [Fact]
        public void CreateOrderShouldAddNewOrderToDb()
        {
            var shipment = new Shipment()
            {
                FromCity = new City { Name = "Plovdiv" },
                ToCity = new City { Name = "Sofia" },
                Length = 2,
                Width = 3,
                Height = 1.2,
                Weight = 333,
                IsExpressDelivery = true,
                IsFragile = false,
                Price = 114.60M
            };

            var input = new AddressInputModel
            {
                ShipmentId = shipment.Id,
                LoadingName = "Fenix",
                UnloadingName = "Fazan",
                LoadingDate = DateTime.UtcNow.AddMinutes(12),
                UnloadingDate = DateTime.UtcNow.AddDays(2),
                LoadingPhone = "0877-223-225",
                UnloadingPhone = "0877-774-225",
                Description = "descr",
                LoadingStreet = "бул. Христо Ботев",
                UnloadingStreet = "Александровска",
                LoadingNumber = "12",
                UnloadingNumber = "23A",
                LoadingEmail = "office@megaimpeks.com",
                UnloadingEmail = "kentavur@gmail.com",
            };

            var user = new ApplicationUser
            {
                UserName = "Ivan"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(optionsBuilder.Options);
            db.Shipments.Add(shipment);
            db.Users.Add(user);
            db.SaveChanges();

            var ordersService = new OrdersService(db);
            ordersService.CreateOrder(input, user.Id);
            var result = db.Orders.Count();

            Assert.Equal(1, result);
        }





    }
}
