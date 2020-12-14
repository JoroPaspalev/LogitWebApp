using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.ChatHub;
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

        public void ChangeAllUnreadMessagesWithThisOrderId(string orderId)
        {
            var unreadMessages = this.db.Messages
                .Where(x => x.OrderId == orderId && x.IsRead == false);

            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
            }

            this.db.SaveChanges();
        }

        public string CreateOrder(AddressInputModel input, string userId)
        {
            var currShipment = this.db.Shipments.FirstOrDefault(s => s.Id == input.ShipmentId);

            var fromCity = this.db.Shipments.Where(s => s.Id == input.ShipmentId).Select(x => new { Name = x.FromCity.Name }).First();

            var toCity = this.db.Shipments.Where(s => s.Id == input.ShipmentId).Select(x => new { Name = x.ToCity.Name })
                .First();

            var loadingAddress = new Address
            {
                Town = fromCity.Name,
                Street = input.LoadingStreet,
                StreetNumber = input.LoadingNumber
            };

            var unloadingAddress = new Address
            {
                Town = toCity.Name,
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

        public ICollection<MessagesViewModel> GetAllMessages(string orderId)
        {
            return this.db.Messages
                .Where(m => m.OrderId == orderId)
                .Select(x => new MessagesViewModel
                {
                    Text = x.Text,
                    CreatedOn = x.CreatedOn,
                    IsAdmin = x.IsAdmin,
                    OrderId = x.OrderId,
                    IsRead = x.IsRead,
                    userWhoSendMessage = x.Order.Creator.UserName
                })
                .ToList();
        }


        public ICollection<MessagesViewModel> GetAllNotReadFromAdminMessages()
        {
            var unreadFromAdminMessages = this.db.Messages
                .Where(m => m.IsAdmin == false && m.IsRead == false)
                .Select(x => new MessagesViewModel
                {
                    CreatedOn = x.CreatedOn,
                    OrderId = x.OrderId,
                    Text = x.Text
                })
                .ToList();

            return unreadFromAdminMessages;
        }
    }
}
