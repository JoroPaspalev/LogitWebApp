using LogitWebApp.Data;
using LogitWebApp.Services.Home;
using LogitWebApp.ViewModels.Offer;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq;
using LogitWebApp.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogitWebApp.Tests.Services
{
    public class HomeServiceTests
    {
        [Fact]
        public void IfCityNotExistShouldReturnFalse()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dataBaseName");

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var homeService = new HomeService(dbContext);

            bool result = homeService.IsThereThatCity(1);

            Assert.False(result);
        }

        [Fact]
        public void CreateShipmentAddNewShipmentToDataBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("nameOfDatabase");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var input = new OfferInputModel()
            {
                From = 1,
                To = 17,
                CountOfPallets = 3,
                Length = 1.74,
                Width = 2,
                Height = 2,
                Weight = 500,
                IsExpressDelivery = true,
                IsFragile = true
            };

            var homeService = new HomeService(db);

            homeService.CreateShipment(input);

            int result = db.Shipments.Count();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task IfCityExistReturnTrue()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dataBaseName");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var homeService = new HomeService(db);

            var currCity = new City()
            {
                Id = 1,
                Name = "Ruse"
            };

            await db.Cities.AddAsync(currCity);
            await db.SaveChangesAsync();

            bool result = homeService.IsThereThatCity(1);

            Assert.True(result);
        }       

        [Fact]
        public async Task GetCitiesShouldReturnAllCitiesInDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("nameOfDatabase");

            var db = new ApplicationDbContext(optionsBuilder.Options);

            var homeService = new HomeService(db);

            var currCities = new List<City>
            {
                new City{Id=1, Name = "Sofia" },
                new City{Id=2, Name = "Ruse"},
                new City{Id=3, Name = "Montana"}
            };

            await db.Cities.AddRangeAsync(currCities);
            await db.SaveChangesAsync();

            int result = homeService.GetCities().Count();

            Assert.Equal(3, result);
        }
    }
}
