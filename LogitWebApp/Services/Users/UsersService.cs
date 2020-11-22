using LogitWebApp.Data;
using LogitWebApp.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserAllShipmentsViewModel> GetAllUserShipments(string userId)
        {
            var AllShipments = this.db.Orders.Where(o => o.CreatorId == "e63484ad-ca2f-4df2-97d3-edbabc8f1b8e")
                .Select(x => new UserAllShipmentsViewModel
                {
                    OrderId = x.Id,
                    From = x.Shipment.From,
                    To = x.Shipment.To,
                    CountOfPallets = x.Shipment.CountOfPallets,
                    Width = x.Shipment.Width,
                    Length = x.Shipment.Length,
                    Height = x.Shipment.Height,
                    Weight = x.Shipment.Weight,
                    Description = x.Shipment.Description,
                    Comment = x.Shipment.Comment,
                    IsDelivered = x.Shipment.IsDelivered,
                    IsFragile = x.Shipment.IsFragile,
                    IsExpressDelivery = x.Shipment.IsExpressDelivery,
                    Sender = x.Shipment.Sender.Name,
                    Receiver = x.Shipment.Receiver.Name,
                    LoadingAddress = x.Shipment.LoadingAddress.Street + " " + x.Shipment.LoadingAddress.StreetNumber,
                    UnloadingAddress = x.Shipment.UnloadingAddress.Street + " " + x.Shipment.UnloadingAddress.StreetNumber,
                    LoadingDate = x.Shipment.LoadingDate,
                    UnloadingDate = x.Shipment.UnloadingDate,
                    OrderCreatedOn = x.Shipment.OrderCreatedOn,
                    Price = x.Shipment.Price.ToString("F2"),
                    DriverFirstName = x.Shipment.Driver.FirstName,
                    DriverLastName = x.Shipment.Driver.LastName
                })
                .OrderByDescending(x => x.OrderCreatedOn)
                .ToList();

            return AllShipments;
        }
    }
}
