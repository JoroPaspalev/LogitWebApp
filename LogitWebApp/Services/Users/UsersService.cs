﻿using LogitWebApp.Data;
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

        public IEnumerable<UserOrderViewModel> GetAllUserOrders(string userId)
        {
            var AllOrders = this.db.Orders.Where(o => o.CreatorId == userId)
                .Select(x => new UserOrderViewModel
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

            return AllOrders;
        }

        public UserOrderViewModel GetOrder(string orderId)
        {
            var currOrder = this.db.Orders.Where(o => o.Id == orderId)
                 .Select(x => new UserOrderViewModel
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
                 .FirstOrDefault();

            return currOrder;
        }
    }
}
