using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Drivers
{
    public class DriversService : IDriversService
    {
        private readonly ApplicationDbContext db;

        public DriversService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddDriver(DriverInputModel input)
        {
            var currDriver = new Driver
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber
            };

            this.db.Drivers.Add(currDriver);
            this.db.SaveChanges();
        }

        public bool IsDriverExist(DriverInputModel input)
        {
            return this.db.Drivers.Any(d => d.FirstName == input.FirstName && d.LastName == input.LastName && d.PhoneNumber == input.PhoneNumber);
        }
    }
}
