using System.Linq;
using LogitWebApp.Data;
using LogitWebApp.ViewModels.Search;
using LogitWebApp.ViewModels.Users;

namespace LogitWebApp.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext db;

        public SearchService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IQueryable<UserOrderViewModel> GetAllUserOrders(string userId, SearchInputModel input)
        {
            var allOrders = this.db.Orders.Where(o => o.CreatorId == userId)
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

            //Ако при празно търсене искам да се показват всички поръчки трябва да изтрия този блок
            //В момента скривам всички поръчки, ако получа празни полета за търсене
            if (input.SenderName == null && input.ReceiverName == null &&
                input.LoadingDate == default && input.UnloadingDate == default)
            {
                return allOrders.Where(o => o.Sender == null);
            }

            if (!string.IsNullOrWhiteSpace(input.SenderName))
            {
                allOrders = allOrders.Where(o => o.Sender.Contains(input.SenderName));
            }

            if (!string.IsNullOrWhiteSpace(input.ReceiverName))
            {
                allOrders = allOrders.Where(o => o.Receiver.Contains(input.ReceiverName));
            }

            if (input.LoadingDate != null)
            {
                allOrders = allOrders.Where(o => o.LoadingDate >= input.LoadingDate);
            }

            if (input.UnloadingDate != null)
            {
                allOrders = allOrders.Where(o => o.UnloadingDate <= input.UnloadingDate);
            }

            return allOrders;
        }
    }
}
