using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Orders;
using LogitWebApp.Services.Users;
using LogitWebApp.ViewModels.Offer;
using LogitWebApp.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogitWebApp.Tests.Services
{
    public class UsersServiceTests
    {
        [Fact]
        public void GetAllUserOrdersShouldReturnAllOrdersFromThisuserId()
        {
            //Create first Order
            var shipment = new Shipment()
            {
                FromCity = new City { Name = "Plovdiv" },
                ToCity = new City { Name = "Sofia" },
                Length = 2,
                Width = 3,
                Height = 1.2,
                Weight = 333,
                CountOfPallets = 2,
                IsExpressDelivery = true,
                IsFragile = false,
                Price = 114.60M
            };

            var input = new AddressInputModel
            {
                ShipmentId = shipment.Id,
                LoadingName = "Fenix",
                UnloadingName = "Faaaaaaaaan",
                LoadingDate = DateTime.UtcNow,
                UnloadingDate = DateTime.UtcNow.AddDays(1),
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

            //Create second Order
            var shipment2 = new Shipment()
            {
                FromCity = new City { Name = "Plovdiv" },
                ToCity = new City { Name = "Sofia" },
                Length = 2,
                Width = 3,
                Height = 1.2,
                Weight = 333,
                CountOfPallets = 2,
                IsExpressDelivery = true,
                IsFragile = false,
                Price = 114.60M
            };

            var input2 = new AddressInputModel
            {
                ShipmentId = shipment2.Id,
                LoadingName = "Fenix2",
                UnloadingName = "Fazan",
                LoadingDate = DateTime.UtcNow,
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

            db.Shipments.Add(shipment2);
            db.SaveChanges();
            ordersService.CreateOrder(input2, user.Id);

            //Create third Order
            var shipment3 = new Shipment()
            {
                FromCity = new City { Name = "Plovdiv" },
                ToCity = new City { Name = "Sofia" },
                Length = 2,
                Width = 3,
                Height = 1.2,
                Weight = 333,
                CountOfPallets = 2,
                IsExpressDelivery = true,
                IsFragile = false,
                Price = 114.60M
            };

            var input3 = new AddressInputModel
            {
                ShipmentId = shipment3.Id,
                LoadingName = "Company",
                UnloadingName = "Fazan",
                LoadingDate = DateTime.UtcNow,
                UnloadingDate = DateTime.UtcNow.AddDays(10),
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

            db.Shipments.Add(shipment3);
            db.SaveChanges();
            ordersService.CreateOrder(input3, user.Id);

            var usersService = new UsersService(db);
            var collection = usersService.GetAllUserOrders(user.Id, 1, 2);
            var result = collection.ItemsCount;
            var pageNumber = collection.PageNumber;
            var itemsPerPage = collection.ItemsPerPage;
            var totalPagesCount = collection.TotalPagesCount;
            var previuosPage = collection.PreviousPage;
            bool hasPrevouspage = collection.HasPreviousPage;
            var nextPage = collection.NextPage;
            bool hasNextPage = collection.HasNextPage;

            Assert.Equal(1, pageNumber);
            Assert.Equal(3, result);
            Assert.Equal(2, itemsPerPage);
            Assert.Equal(2, totalPagesCount);
            Assert.Equal(0, previuosPage);
            Assert.False(hasPrevouspage);
            Assert.Equal(2, nextPage);
            Assert.True(hasNextPage);
        }


        [Fact]
        public void GetOrderShouldreturnTheOrderIfItExist()
        {
            //Create first Order
            var shipment = new Shipment()
            {
                FromCity = new City { Name = "Plovdiv" },
                ToCity = new City { Name = "Sofia" },
                Length = 2,
                Width = 3,
                Height = 1.2,
                Weight = 333,
                CountOfPallets = 2,
                IsExpressDelivery = true,
                IsFragile = false,
                Price = 114.60M
            };

            var input = new AddressInputModel
            {
                ShipmentId = shipment.Id,
                LoadingName = "Fenix",
                UnloadingName = "Faaaaaaaaan",
                LoadingDate = DateTime.UtcNow,
                UnloadingDate = DateTime.UtcNow.AddDays(1),
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

            //Create second Order
            var shipment2 = new Shipment()
            {
                FromCity = new City { Name = "Plovdiv" },
                ToCity = new City { Name = "Sofia" },
                Length = 2,
                Width = 3,
                Height = 1.2,
                Weight = 333,
                CountOfPallets = 2,
                IsExpressDelivery = true,
                IsFragile = false,
                Price = 114.60M
            };

            var input2 = new AddressInputModel
            {
                ShipmentId = shipment2.Id,
                LoadingName = "Fenix2",
                UnloadingName = "Fazan",
                LoadingDate = DateTime.UtcNow,
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

            db.Shipments.Add(shipment2);
            db.SaveChanges();
            ordersService.CreateOrder(input2, user.Id);

            //Create third Order
            var shipment3 = new Shipment()
            {
                FromCity = new City { Name = "Plovdiv" },
                ToCity = new City { Name = "Sofia" },
                Length = 2,
                Width = 3,
                Height = 1.2,
                Weight = 333,
                CountOfPallets = 2,
                IsExpressDelivery = true,
                IsFragile = false,
                Price = 114.60M
            };

            var input3 = new AddressInputModel
            {
                ShipmentId = shipment3.Id,
                LoadingName = "Company",
                UnloadingName = "Fazan",
                LoadingDate = DateTime.UtcNow,
                UnloadingDate = DateTime.UtcNow.AddDays(10),
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

            db.Shipments.Add(shipment3);
            db.SaveChanges();
            ordersService.CreateOrder(input3, user.Id);
            var orderId = db.Orders.First().Id;

            var usersService = new UsersService(db);
            var model = usersService.GetOrder(orderId);
            var fromCity = model.From;

            Assert.NotNull(model);
            Assert.Equal("Plovdiv", fromCity);



        }




    }
}
