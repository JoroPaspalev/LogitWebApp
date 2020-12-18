using System;
using System.Linq;
using Xunit;
using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Orders;
using LogitWebApp.Services.Search;
using LogitWebApp.ViewModels.Offer;
using LogitWebApp.ViewModels.Search;
using Microsoft.EntityFrameworkCore;

namespace LogitWebApp.Tests.Services
{
    public class SearchServiceTests
    {
        [Fact]
        public void GetAllUserOrdersShouldReturnOnlyOrdersWhichMatchConditions()
        {
            var searchModel = new SearchInputModel
            {
                SenderName = "Fenix",
            };

            //Create first order
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

            //Create second order
            var shipment2 = new Shipment()
            {
                FromCity = new City { Name = "Ruse" },
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
                LoadingName = "Fenix",
                UnloadingName = "Alto",
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
            db.Shipments.Add(shipment2);
            db.SaveChanges();
            ordersService.CreateOrder(input2, user.Id);

            var searchService = new SearchService(db);
            var collection = searchService.GetAllUserOrders(user.Id, searchModel);

            var result = collection.Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetAllUserOrdersShouldReturnNothingWhenEmptyFieldsAreGiven()
        {
            var searchModel = new SearchInputModel
            {
                SenderName = null,
                ReceiverName = null,
                LoadingDate = null,
                UnloadingDate = null,
            };

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

            var searchService = new SearchService(db);
            var collection = searchService.GetAllUserOrders(user.Id, searchModel);

            var result = collection.Count();

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetAllUserOrdersShouldReturnAllOrdersWithGiverReceiver()
        {
            var searchModel = new SearchInputModel
            {
                SenderName = null,
                ReceiverName = "Faz",
                LoadingDate = null,
                UnloadingDate = null,
            };

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

            db.Shipments.Add(shipment3);
            db.SaveChanges();
            ordersService.CreateOrder(input3, user.Id);

            var searchService = new SearchService(db);
            var collection = searchService.GetAllUserOrders(user.Id, searchModel);

            var result = collection.Count();

            Assert.Equal(2, result);           
        }

        [Fact]
        public void GetAllUserOrdersShouldReturnAllOrdersAfterGivenLoadingDate()
        {
            var searchModel = new SearchInputModel
            {
                SenderName = null,
                ReceiverName = null,
                LoadingDate = DateTime.UtcNow.AddMinutes(25),
                UnloadingDate = null,
            };

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
                LoadingDate = DateTime.UtcNow.AddDays(1),
                UnloadingDate = DateTime.UtcNow.AddDays(5),
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
                LoadingDate = DateTime.UtcNow.AddDays(2),
                UnloadingDate = DateTime.UtcNow.AddDays(4),
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

            db.Shipments.Add(shipment3);
            db.SaveChanges();
            ordersService.CreateOrder(input3, user.Id);

            var searchService = new SearchService(db);
            var collection = searchService.GetAllUserOrders(user.Id, searchModel);

            var result = collection.Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetAllUserOrdersShouldReturnAllOrdersBeforeGivenUnloadingDate()
        {
            var searchModel = new SearchInputModel
            {
                SenderName = null,
                ReceiverName = null,
                LoadingDate = null,
                UnloadingDate = DateTime.UtcNow.AddDays(5),
            };

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

            var searchService = new SearchService(db);
            var collection = searchService.GetAllUserOrders(user.Id, searchModel);

            var result = collection.Count();

            Assert.Equal(2, result);
        }

    }
}
