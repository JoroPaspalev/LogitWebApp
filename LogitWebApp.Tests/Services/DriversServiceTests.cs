using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Drivers;
using LogitWebApp.ViewModels.Drivers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogitWebApp.Tests.Services
{
    public class DriversServiceTests
    {
        [Fact]
        public async Task AddDriverShouldCreateNewDriverInDbAndPutItToDriverRole()
        {
            List<ApplicationUser> _users = new List<ApplicationUser>();

            var input = new DriverInputModel()
            {
                Email = "aaa@abv.bg",
                Password = "123456",
                FirstName = "Pesho",
                LastName = "Ivanov",
                PhoneNumber = "0888-123-456"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            //Трябва ми някой който да ме даде UserManager
            UserManager<ApplicationUser> _userManager = MockUserManager<ApplicationUser>(_users).Object;

            var driversService = new DriversService(db, _userManager);
            await driversService.AddDriverAsync(input);
            int result = _users.Count;
            string resultFirstName = _users.FirstOrDefault(u => u.FirstName == "Pesho").FirstName;

            Assert.Equal(1, result);
            Assert.Equal("Pesho", resultFirstName);
        }

        [Fact]
        public async Task GetAllShipmentsShouldReturnExactCountOfShipmentsPerPage()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);

            //Направи ми 5 shipments и ги добави в db.Shipments
            var shipments = new List<Shipment>()
            {
                new Shipment()
                {
                    CountOfPallets = 333,
                    LoadingDate = DateTime.UtcNow.AddDays(1),
                    LoadingAddress = new Address(){Town = "Sofia"},
                    UnloadingAddress = new Address(){Town = "Ruse"}
                },
                new Shipment()
                {
                    CountOfPallets = 444,
                    LoadingDate = DateTime.UtcNow.AddDays(2),
                    LoadingAddress = new Address(){Town = "Ruse"},
                    UnloadingAddress = new Address(){Town = "Varna"}
                },
                new Shipment()
                {
                    CountOfPallets = 555,
                    LoadingDate = DateTime.UtcNow.AddDays(3),
                    LoadingAddress = new Address(){Town = "Plovdiv"},
                    UnloadingAddress = new Address(){Town = "Ruse"}
                },
                 new Shipment()
                {
                    CountOfPallets = 666,
                    LoadingDate = DateTime.UtcNow.AddDays(4),
                    LoadingAddress = new Address(){Town = "Burgas"},
                    UnloadingAddress = new Address(){Town = "Ruse"}
                },
                 new Shipment()
                {
                    CountOfPallets = 777,
                    LoadingDate = DateTime.UtcNow.AddDays(5),
                    LoadingAddress = new Address(){Town = "Aitos"},
                    UnloadingAddress = new Address(){Town = "Vidin"}
                }
            };
            await db.Shipments.AddRangeAsync(shipments);
            await db.SaveChangesAsync();

            var driversService = new DriversService(db);

            var result = driversService.GetAllShipments(2, 3);

            //Assert that count of Shipment per this page are 2
            Assert.Equal(2, result.ShipmentsOfCurrPage.Count());
        }

        [Fact]
        public async Task GetMyShipmentsShouldReturnOnlyThisDriverShipments()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);

            var currDriver = new ApplicationUser()
            {
                FirstName = "Kiro",
                LastName = "Kirov"
            };

            await db.Users.AddAsync(currDriver);
            await db.SaveChangesAsync();

            //Направи ми 5 shipments и ги добави в db.Shipments
            var shipments = new List<Shipment>()
            {
                new Shipment()
                {
                    CountOfPallets = 333,
                    LoadingDate = DateTime.UtcNow.AddDays(1),
                    LoadingAddress = new Address(){Town = "Sofia"},
                    UnloadingAddress = new Address(){Town = "Ruse"},
                    Sender = new Participant(){Name = "Company1"},
                    Receiver = new Participant(){Name = "Company2"},
                    DriverId = currDriver.Id
                },
                new Shipment()
                {
                    CountOfPallets = 444,
                    LoadingDate = DateTime.UtcNow.AddDays(2),
                    LoadingAddress = new Address(){Town = "Ruse"},
                    UnloadingAddress = new Address(){Town = "Varna"},
                    Sender = new Participant(){Name = "Company1"},
                    Receiver = new Participant(){Name = "Company2"},
                    DriverId = currDriver.Id
                },
                new Shipment()
                {
                    CountOfPallets = 555,
                    LoadingDate = DateTime.UtcNow.AddDays(3),
                    LoadingAddress = new Address(){Town = "Plovdiv"},
                    UnloadingAddress = new Address(){Town = "Ruse"},
                    Sender = new Participant(){Name = "Company1"},
                    Receiver = new Participant(){Name = "Company2"},
                    Driver = currDriver
                },
                 new Shipment()
                {
                    CountOfPallets = 666,
                    LoadingDate = DateTime.UtcNow.AddDays(4),
                    LoadingAddress = new Address(){Town = "Plovdiv"},
                    UnloadingAddress = new Address(){Town = "Ruse"},
                    Sender = new Participant(){Name = "Company1"},
                    Receiver = new Participant(){Name = "Company2"},
                    Driver = currDriver
                }
            };
            await db.Shipments.AddRangeAsync(shipments);
            await db.SaveChangesAsync();

            var driversService = new DriversService(db);

            var result = driversService.GetMyShipments(currDriver.Id, 2, 2);

            //Assert that count of Shipments per this page is 1
            Assert.Equal(2, result.DriverShipmentsOfCurrentPage.Count());
        }

        [Fact]
        public async Task GetShipmentShouldReturnSearchedShipment()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);

            var currShipment = new Shipment()
            {
                CountOfPallets = 333,
                LoadingDate = DateTime.UtcNow.AddDays(1),
                LoadingAddress = new Address() { Town = "Sofia" },
                UnloadingAddress = new Address() { Town = "Ruse" },
                Sender = new Participant() { Name = "Company1" },
                Receiver = new Participant() { Name = "Company2" }
            };

            //Направи ми 5 shipments и ги добави в db.Shipments
            var shipments = new List<Shipment>()
            {
                new Shipment()
                {
                    CountOfPallets = 444,
                    LoadingDate = DateTime.UtcNow.AddDays(2),
                    LoadingAddress = new Address(){Town = "Ruse"},
                    UnloadingAddress = new Address(){Town = "Varna"},
                    Sender = new Participant(){Name = "Company1"},
                    Receiver = new Participant(){Name = "Company2"}
                },
                new Shipment()
                {
                    CountOfPallets = 555,
                    LoadingDate = DateTime.UtcNow.AddDays(3),
                    LoadingAddress = new Address(){Town = "Plovdiv"},
                    UnloadingAddress = new Address(){Town = "Ruse"},
                    Sender = new Participant(){Name = "Company1"},
                    Receiver = new Participant(){Name = "Company2"}
                },
                 new Shipment()
                {
                    CountOfPallets = 666,
                    LoadingDate = DateTime.UtcNow.AddDays(4),
                    LoadingAddress = new Address(){Town = "Plovdiv"},
                    UnloadingAddress = new Address(){Town = "Ruse"},
                    Sender = new Participant(){Name = "Company1"},
                    Receiver = new Participant(){Name = "Company2"}
                }
            };
            shipments.Add(currShipment);
            await db.Shipments.AddRangeAsync(shipments);
            await db.SaveChangesAsync();

            var driversService = new DriversService(db);

            var result = driversService.GetShipment(currShipment.Id);

            //Assert that count of Shipments per this page is 1
            Assert.NotNull(result);
        }

        [Fact]
        public async Task IsPhoneExistShouldReturnPhoneThePhoneIsInDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            await db.Users.AddAsync(new ApplicationUser { PhoneNumber = "0888-111-222" });
            await db.SaveChangesAsync();

            var driversService = new DriversService(db);
            bool result = driversService.IsPhoneExist("0888-111-222");

            Assert.True(result);
        }

        [Fact]
        public async Task IsPhoneNotExistShouldReturnFalse()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            await db.Users.AddAsync(new ApplicationUser { PhoneNumber = "0888-111-222" });
            await db.SaveChangesAsync();

            var driversService = new DriversService(db);
            bool result = driversService.IsPhoneExist("0888-111-999");

            Assert.False(result);
        }

        [Fact]
        public async Task IsDriverExistShouldReturnTrue()
        {
            var currUser = new ApplicationUser() { Email = "aaa@aa.bb" };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            await db.Users.AddAsync(currUser);
            await db.SaveChangesAsync();

            var driversService = new DriversService(db);
            bool result = driversService.IsDriverExist("aaa@aa.bb");

            Assert.True(result);
        }

        [Fact]
        public async Task IsDriverNotExistShouldReturnFalse()
        {
            var currUser = new ApplicationUser() { Email = "aaa@aa.bb" };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            await db.Users.AddAsync(currUser);
            await db.SaveChangesAsync();

            var driversService = new DriversService(db);
            bool result = driversService.IsDriverExist("zzz@zz.zz");

            Assert.False(result);
        }

        [Fact]
        public async Task ChangeShipmentDataShouldReplaceCurrShipmentWithNewData()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            var currShipment = new Shipment
            {
                Width = 2,
                Length = 2,
                Height = 2,
                Weight = 200,
                CountOfPallets = 3,
                Comment = "Some Text",
                IsDelivered = true
            };
            await db.Shipments.AddAsync(currShipment);
            await db.SaveChangesAsync();

            var editShipment = new EditShipment
            {
                ShipmentId = currShipment.Id,
                Width = 4,
                Length = 4,
                Height = 4,
                Weight = 400,
                CountOfPallets = 4,
                Comment = "Editted Text",
                IsDelivered = false
            };

            var driversService = new DriversService(db);
            Task result = driversService.ChangeShipmentDataAsync(editShipment);

            Assert.True(result.IsCompletedSuccessfully);
            Assert.Equal(400, currShipment.Weight);
        }

        [Fact]
        public async Task AttachShipmentToDriverShouldChainDriverWithShipment()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            var currShipment = new Shipment
            {
                Width = 2,
                Length = 2,
                Height = 2,
                Weight = 200,
                CountOfPallets = 3,
                Comment = "Some Text",
                IsDelivered = true
            };
            await db.Shipments.AddAsync(currShipment);
            await db.SaveChangesAsync();

            var driver = new ApplicationUser
            {
                FirstName = "Ivan"
            };

            var driversService = new DriversService(db);
            await driversService.AttachShipmentToDriverAsync(currShipment.Id, driver.Id);

            Assert.Equal(currShipment.DriverId, driver.Id);
        }

        [Fact]
        public async Task DeleteDriverShouldDeleteDriverFromDb()
        {
            List<ApplicationUser> _users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Email = "aaa@abv.bg",
                    FirstName = "Pesho",
                    LastName = "Ivanov",
                    PhoneNumber = "0888-123-456"
                },
                new ApplicationUser()
                {
                    Email = "rrr@rrr.bg",
                    FirstName = "Misho",
                    LastName = "Mishov",
                    PhoneNumber = "0999-999-999"
                }
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("name");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            //Трябва ми някой който да ме даде UserManager
            UserManager<ApplicationUser> _userManager = MockUserManager<ApplicationUser>(_users).Object;

            var driversService = new DriversService(_userManager);
            await driversService.DeleteDriverAsync("aaa@abv.bg");
            int result = _users.Count;
            string resultFirstName = _users.FirstOrDefault(u => u.FirstName == "Pesho").FirstName;

            Assert.Equal(1, result);
        }

        //Този метод ми създава userManager, който записва не в db а в подаден List<ApplicationUser> т.е. все едно данните за създадените Users не са в db.Users а са в този лист, който ни е достъпен
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> users) where TUser : class
        {
            //Това казва Създай ми фалшив List<ApplicationUser>
            var store = new Mock<IUserStore<TUser>>();
            //Това казва Създай ми фалшив UserManager от ApplicationUser
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            //Когато някой ти поиска Count на db.Users.Count, вместо това, ти му върни на fakeDbUsers.Count
            //mgr.Setup(x => x.Users.Count()).Returns(users.Count);
            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => users.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

    }
}
