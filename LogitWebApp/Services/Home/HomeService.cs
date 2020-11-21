using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext db;

        public HomeService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string CreateShipment(OfferInputModel input)
        {
            var currShipment = new Shipment()
            {
                From = input.From,
                To = input.To,
                CountOfPallets = input.CountOfPallets,
                Length = input.Length,
                Width = input.Width,
                Height = input.Height,
                Weight = input.Weight,
                IsExpressDelivery = input.IsExpressDelivery,
                IsFragile = input.IsFragile
            };

            this.db.Shipments.Add(currShipment);
            this.db.SaveChanges();
            return currShipment.Id;
        }
    }
}
