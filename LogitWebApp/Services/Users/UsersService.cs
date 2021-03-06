﻿using System.Linq;
using LogitWebApp.Data;
using LogitWebApp.ViewModels.Pagination;
using LogitWebApp.ViewModels.Users;

namespace LogitWebApp.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public PaginationViewModel GetAllUserOrders(string userId, int id, int items = 6)
        {
            var ordersPerPage = this.db.Orders.Where(o => o.CreatorId == userId)
                .OrderByDescending(x => x.Shipment.OrderCreatedOn)
                .Skip((id - 1) * items)
                .Take(items)
                .Select(x => new UserOrderViewModel
                {
                    OrderId = x.Id,
                    From = x.Shipment.FromCity.Name,
                    To = x.Shipment.ToCity.Name,
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
                    DriverId = x.Shipment.DriverId,
                    DriverFirstName = x.Shipment.Driver.FirstName,
                    DriverLastName = x.Shipment.Driver.LastName,
                    Images = x.Shipment.Images.ToList(),
                    VotesCount = x.Shipment.Driver.DriverVotes.Count,
                    VotesTotal = x.Shipment.Driver.DriverVotes.Sum(x => x.Value)
                });

            var paginationViewModel = new OrdersPaginationViewModel
            {
                PageNumber = id,
                ItemsCount = this.db.Orders.Where(o => o.CreatorId == userId).Count(),
                ItemsPerPage = items,
                Orders = ordersPerPage,
            };

            return paginationViewModel;
        }

        public UserOrderViewModel GetOrder(string orderId)
        {
            var currOrder = this.db.Orders.Where(o => o.Id == orderId)
                 .Select(x => new UserOrderViewModel
                 {
                     OrderId = x.Id,
                     From = x.Shipment.FromCity.Name,
                     To = x.Shipment.ToCity.Name,
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
                     DriverLastName = x.Shipment.Driver.LastName,
                     Images = x.Shipment.Images.ToList()
                 })
                 .FirstOrDefault();

            return currOrder;
        }
    }
}
