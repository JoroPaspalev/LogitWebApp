using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;

        public OrdersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string CreateOrder(AddressInputModel input, string userId)
        {
            var currShipment = this.db.Shipments.FirstOrDefault(s=>s.Id == input.ShipmentId);

            var loadingAddress = new Address
            {
                Town = currShipment.From,
                Street = input.LoadingStreet,
                StreetNumber = input.LoadingNumber,

            };

            var unloadingAddress = new Address
            {
                Town = currShipment.To,
                Street = input.UnloadingStreet,
                StreetNumber = input.UnloadingNumber
            };

            this.db.Addresses.Add(loadingAddress);
            this.db.Addresses.Add(unloadingAddress);

            var sender = new Participant
            {
                Name = input.LoadingName,
                Phone = input.LoadingPhone,
                Email = input.LoadingEmail
            };

            var receiver = new Participant
            {
                Name = input.UnloadingName,
                Phone = input.UnloadingPhone,
                Email = input.UnloadingEmail
            };

            this.db.Participants.Add(sender);
            this.db.Participants.Add(receiver);
            this.db.SaveChanges();

            //DateTime parsedLoadingDate;
            //bool isParsed = DateTime.TryParseExact(input.LoadingDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedLoadingDate);

            //DateTime parsedUnloadingDate;
            //bool isParsedUnloadingDate = DateTime.TryParseExact(input.UnloadingDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedUnloadingDate);

            Shipment shipment = this.db.Shipments.FirstOrDefault(x => x.Id == input.ShipmentId);

            shipment.LoadingAddress = loadingAddress;
            shipment.UnloadingAddress = unloadingAddress;
            shipment.Sender = sender;
            shipment.Receiver = receiver;
            shipment.OrderCreatedOn = DateTime.UtcNow;
            shipment.LoadingDate = input.LoadingDate;
            shipment.UnloadingDate = input.UnloadingDate;
            shipment.Description = input.Description;

            var currOrder = new Order
            {
                //Creator = this.UserId - когато имам Login
                CreatorId = userId,
                Shipment = shipment
            };

            this.db.Orders.Add(currOrder);
            this.db.SaveChanges();

            return currOrder.Id;

        }
    }
}
