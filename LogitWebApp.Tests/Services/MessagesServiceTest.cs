using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Messages;
using LogitWebApp.Services.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogitWebApp.Tests.Services
{
    public class MessagesServiceTest
    {       
        [Fact]
        public void GetAllUnreadMessagesShouldReturnExactNumberOfUnreadedMessages()
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
                .UseInMemoryDatabase("GetAllUnreadMessages");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            db.Orders.Add(currOrder);
            db.SaveChanges();

            var ordersService = new OrdersService(db);

            var messagesService = new MessagesService(db, ordersService);

            var collection = messagesService.GetAllUnreadMessages().First();
            var result = collection.CountUnreadMessages;

            Assert.Equal(3, result);
        }
    }
}
