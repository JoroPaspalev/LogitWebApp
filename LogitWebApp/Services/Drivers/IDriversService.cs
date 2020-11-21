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
        public Task AddDriver(DriverInputModel input);

        public bool IsDriverExist(string email);

        public bool IsPhoneExist(string phone);

        public IEnumerable<AllShipmentsWithAddresses> GetAllShipments();

        public IEnumerable<AllShipmentsWithAddresses> GetMyShipments(string driverId);

        public void ChangeShipmentData(EditShipment input);

        public AllShipmentsWithAddresses GetShipment(string shipmentId);

        public void AttachShipmentToDriver(string shipmentId, string userId);

        public Task<bool> DeleteDriver(string email);
    }
}
