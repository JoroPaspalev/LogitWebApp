using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Offers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogitWebApp.Tests.Services
{
    public class OffersServiceTests
    {
        [Fact]
        public async Task GetOfferShouldGiveMeRealOffer()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var distance = new Distance()
            {
                Id = 1,
                FromCity = "Sofia",
                ToCity = "Ruse",
                DistanceInKM = 304.1
            };
            await db.Distances.AddRangeAsync(distance);
            await db.SaveChangesAsync();

            var currShipment = new Shipment()
            {
                FromCity = new City { Id = 1, Name = "Sofia" },
                ToCity = new City { Id = 2, Name = "Ruse" },
                IsExpressDelivery = true,
                IsFragile = true,
                CountOfPallets = 2,
                Length = 1.30,
                Width = 0.80,
                Weight = 600
            };

            await db.Shipments.AddAsync(currShipment);
            await db.SaveChangesAsync();

            //Price 208lv

            var offerService = new OffersService(db);
            var result = offerService.GetOffer(currShipment);

            Assert.Equal(208, Math.Round((double)result.Result.Price, 2));
        }

        [Fact]
        public async Task GetOfferShouldGiveMeRealOfferWith3Pallets()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var distance = new Distance()
            {
                Id = 1,
                FromCity = "Sofia",
                ToCity = "Ruse",
                DistanceInKM = 304.1
            };
            await db.Distances.AddRangeAsync(distance);
            await db.SaveChangesAsync();

            var currShipment = new Shipment()
            {
                FromCity = new City { Id = 1, Name = "Sofia" },
                ToCity = new City { Id = 2, Name = "Ruse" },
                IsExpressDelivery = false,
                IsFragile = false,
                CountOfPallets = 3,
                Width = 0.80,
                Length = 1.30,
                Height = 1,
                Weight = 600
            };

            await db.Shipments.AddAsync(currShipment);
            await db.SaveChangesAsync();

            //Price 246.32lv

            var offerService = new OffersService(db);
            var result = offerService.GetOffer(currShipment);

            Assert.Equal(246.32, Math.Round((double)result.Result.Price, 2));
        }

        [Fact]
        public async Task GetOfferShouldGiveMeRealOfferWith4OrMorePallets()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var db = new ApplicationDbContext(optionsBuilder.Options);
            var distance = new Distance()
            {
                Id = 1,
                FromCity = "Sofia",
                ToCity = "Ruse",
                DistanceInKM = 304.1
            };
            await db.Distances.AddRangeAsync(distance);
            await db.SaveChangesAsync();

            var currShipment = new Shipment()
            {
                FromCity = new City { Id = 1, Name = "Sofia" },
                ToCity = new City { Id = 2, Name = "Ruse" },
                IsExpressDelivery = false,
                IsFragile = false,
                CountOfPallets = 4,
                Width = 0.80,
                Length = 1.30,
                Height = 1,
                Weight = 600
            };

            await db.Shipments.AddAsync(currShipment);
            await db.SaveChangesAsync();

            //Price  310.18lv

            var offerService = new OffersService(db);
            var result = offerService.GetOffer(currShipment);

            Assert.Equal(310.18, Math.Round((double)result.Result.Price, 2));
        }


        [Fact]
        public async Task GetShipmentByIdShouldReturnShipmentIfExist()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dbName");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var currShipment = new Shipment();
            var secondShipment = new Shipment();
            await db.Shipments.AddAsync(currShipment);
            await db.Shipments.AddAsync(secondShipment);
            await db.SaveChangesAsync();

            var offerService = new OffersService(db);

            var result = offerService.GetShipmentById(currShipment.Id);

            Assert.Equal(currShipment, result);
        }

        [Fact]
        public async Task GetShipmentByIdShouldReturnNullIfShipmentNotExist()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dbName");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var currShipment = new Shipment();
            var secondShipment = new Shipment();
            await db.Shipments.AddAsync(currShipment);
            await db.Shipments.AddAsync(secondShipment);
            await db.SaveChangesAsync();

            var offerService = new OffersService(db);

            var result = offerService.GetShipmentById(Guid.NewGuid().ToString());

            Assert.NotEqual(currShipment, result);
        }



    }
}
