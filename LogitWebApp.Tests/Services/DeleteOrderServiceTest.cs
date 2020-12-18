using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.DeleteOrder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LogitWebApp.Tests.Services
{
    public class DeleteOrderServiceTest
    {
        [Fact]
        public async Task DeleteOrderShouldDeleteOrderFromDbAndAllConnectedMessagesWithThisOrder()
        {
            //5. Правя нов DbContextOptionsBuilder, който казва че настройките са му да работи във виртуална база данни а не в Sql база
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            //3. Правя си нов DbContex, който да подам на deleteOrderService
            //4. Но при направата на DbContext, пък ми трябва optionsBuilder
            var db = new ApplicationDbContext(optionsBuilder.Options);

            //1. Правя си инстанция на service, който ще тествам. 
            //2. На тази инстанция и трябва db
            var deleteOrderService = new DeleteOrderService(db);

            var currShipment = new Shipment()
            {
                CountOfPallets = 777,
                LoadingDate = DateTime.UtcNow.AddDays(5),
                LoadingAddress = new Address() { Town = "Aitos" },
                UnloadingAddress = new Address() { Town = "Vidin" }
            };

            var currorder = new Order
            {
                Shipment = currShipment,
            };

            var messages = new List<Message>()
            {
                new Message{Text = "Hello", Order = currorder, CreatedOn = DateTime.UtcNow },
                new Message{Text = "Can I ask you a question", Order = currorder, CreatedOn = DateTime.UtcNow },
            };

            currorder.Messages = messages;

            await db.Orders.AddAsync(currorder);
            await db.SaveChangesAsync();


            bool result = deleteOrderService.DeleteOrderAsync(currorder.Id).Result;

            Assert.True(result);
            Assert.Equal(0, db.Orders.Count());
        }

        [Fact]
        public void IsOrderExistShouldreturnTrueIfOrderExist()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(optionsBuilder.Options);
            var deleteOrderService = new DeleteOrderService(db);

            var currShipment = new Shipment()
            {
                CountOfPallets = 777,
                LoadingDate = DateTime.UtcNow.AddDays(5),
                LoadingAddress = new Address() { Town = "Aitos" },
                UnloadingAddress = new Address() { Town = "Vidin" }
            };

            var currorder = new Order
            {
                Shipment = currShipment,
            };

            db.Orders.Add(currorder);
            db.SaveChanges();

            bool result = deleteOrderService.IsOrderExist(currorder.Id);

            Assert.True(result);
        }

        [Fact]
        public void IsOrderExistShouldreturnFalseIfOrderNotExist()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(optionsBuilder.Options);
            var deleteOrderService = new DeleteOrderService(db);

            var currShipment = new Shipment()
            {
                CountOfPallets = 777,
                LoadingDate = DateTime.UtcNow.AddDays(5),
                LoadingAddress = new Address() { Town = "Aitos" },
                UnloadingAddress = new Address() { Town = "Vidin" }
            };

            var currorder = new Order
            {
                Shipment = currShipment,
            };

            db.Orders.Add(currorder);
            db.SaveChanges();

            bool result = deleteOrderService.IsOrderExist(Guid.NewGuid().ToString());

            Assert.False(result);
        }
    }
}
