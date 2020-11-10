using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Drivers
{
    public interface IDriversService
    {
        public void AddDriver(DriverInputModel input);

        public bool IsDriverExist(DriverInputModel input);

        public IEnumerable<AllShipmentsWithAddresses> GetAllShipments();
    }
}
