using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Offers
{
    public class OffersService : IOffersService
    {
        private readonly ApplicationDbContext db;
        private const int countOfPalletsInOneTruck = 30;
        private const decimal costPerKilometer = 2M; //Costs are 2lv/km
        private const decimal profit = 2;

        public OffersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Shipment GetOffer(OfferInputModel input)
        {
            double kilometers = this.db.Distances
                .Where(d => d.FromCity.ToLower() == input.From.ToLower() && d.ToCity.ToLower() == input.To.ToLower())
                .Select(x => x.DistanceInKM)
                .First();

            decimal costForOnePallet = (decimal)(kilometers) * costPerKilometer / countOfPalletsInOneTruck;
            decimal cellingPricePerPallet = costForOnePallet * profit;

            if (input.IsExpressDelivery == "on")
            {
                cellingPricePerPallet *= 1.20M;
            }

            if (input.CountOfPallets == 2)
            {
                //5% discount when are 2 pallets
                cellingPricePerPallet *= 0.95M;
            }

            if (input.CountOfPallets == 3)
            {
                //10% discount when are 3 pallets
                cellingPricePerPallet *= 0.9M;
            }

            if (input.CountOfPallets > 4)
            {
                //15% discount for more than 4 pallets
                cellingPricePerPallet *= 0.85M;
            }

            if ((input.Length > 1.20 && input.Width <= 0.8) || (input.Length <= 1.20 && input.Width > 0.8))
            {
                //When pallets are over size
                cellingPricePerPallet *= 1.5M;
            }

            if (input.Weight >= 500 && input.Weight <= 1000)
            {
                //When pallets are over weight
                cellingPricePerPallet *= 1.5M;
            }

            var currShipment = new Shipment
            {
                Price = (cellingPricePerPallet * input.CountOfPallets),
                CountOfPallets = input.CountOfPallets,
                From = input.From,
                To = input.To,
                Length = input.Length,
                Width = input.Width,
                Height = input.Height,
                Weight = input.Weight,                             
                IsExpressDelivery = input.IsExpressDelivery == "on",
                IsFragile = input.IsFragile == "on"
            };

            this.db.Shipments.Add(currShipment);
            this.db.SaveChanges();

            return currShipment;
        }
    }
}
