using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Drivers;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public IEnumerable<AllShipmentsWithAddresses> GetAllShipments()
        {
            return this.db.Shipments
                .Where(s => s.IsDelivered == false && s.LoadingAddress != null && s.UnloadingAddress != null && s.DriverId == null)
                .Select(x => new AllShipmentsWithAddresses
                {
                    Id = x.Id,                    
                    Length = x.Length,
                    Width = x.Width,
                    Height = x.Height,
                    Weight = x.Weight,
                    CountOfPallets = x.CountOfPallets,
                    LoadingAddress = x.LoadingAddress.ToString(),
                    UnloadingAddress = x.UnloadingAddress.ToString(),
                    IsFragile = x.IsFragile == true ? "Да": "Не",
                    LoadingDate = x.LoadingDate.ToString().Substring(0, 10),
                    UnloadingDate = x.UnloadingDate.ToString().Substring(0, 10)                                        
                })
                .ToList();

        }

        public bool IsDriverExist(DriverInputModel input)
        {
            return this.db.Drivers.Any(d => d.FirstName == input.FirstName && d.LastName == input.LastName && d.PhoneNumber == input.PhoneNumber);
        }
    }
}
