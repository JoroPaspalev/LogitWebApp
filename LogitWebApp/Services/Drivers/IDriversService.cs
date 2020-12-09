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
        public Task AddDriverAsync(DriverInputModel input);

        public bool IsDriverExist(string email);

        public bool IsPhoneExist(string phone);

        public ShipmentsPaginationViewModel GetAllShipments(int id, int items);

        public DriverShipmentsPaginationViewModel GetMyShipments(string driverId, int id, int itemsPerPage);

        public Task ChangeShipmentDataAsync(EditShipment input);

        public AllShipmentsWithAddresses GetShipment(string shipmentId);

        public Task AttachShipmentToDriverAsync(string shipmentId, string userId);

        public Task<bool> DeleteDriverAsync(string email);
    }
}
