using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using static LogitWebApp.Common.GlobalConstants;
using System.Linq;


namespace LogitWebApp.Services.Offers
{
    public class OffersService : IOffersService
    {
        private readonly ApplicationDbContext db;       

        public OffersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Shipment GetOffer(Shipment input)
        {
            double kilometers = this.db.Distances
                .Where(d =>
                (d.FromCity.ToLower() == input.From.ToLower() && d.ToCity.ToLower() == input.To.ToLower())
                ||
                (d.FromCity.ToLower() == input.To.ToLower() && d.ToCity.ToLower() == input.From.ToLower())
                )
                .Select(x => x.DistanceInKM)
                .FirstOrDefault();

            decimal costForOnePallet = (decimal)(kilometers) * costPerKilometer / countOfPalletsInOneTruck;
            decimal cellingPricePerPallet = costForOnePallet * profit;

            if (input.IsExpressDelivery)
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

            input.Price = cellingPricePerPallet * input.CountOfPallets;
            this.db.SaveChanges();
           
            return input;
        }

        public Shipment GetShipmentById(string shipmentId)
        {
            return this.db.Shipments.FirstOrDefault(s => s.Id == shipmentId);
        }
    }
}
