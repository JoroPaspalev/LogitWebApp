using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Offer;
using static LogitWebApp.Common.GlobalConstants;

namespace LogitWebApp.Services.Offers
{
    public class OffersService : IOffersService
    {
        private readonly ApplicationDbContext db;

        public OffersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ShipmentViewModel> GetOffer(Shipment input)
        {
            var fromCity = this.db.Shipments.Where(s => s.Id == input.Id).Select(x => new { Name = x.FromCity.Name }).First();

            var toCity = this.db.Shipments.Where(s => s.Id == input.Id).Select(x => new { Name = x.ToCity.Name })
                .First();

            double kilometers = this.db.Distances
                .Where(d =>
                (d.FromCity.ToLower() == fromCity.Name.ToLower() && d.ToCity.ToLower() == toCity.Name.ToLower())
                ||
                (d.FromCity.ToLower() == toCity.Name.ToLower() && d.ToCity.ToLower() == fromCity.Name.ToLower())
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

            if (input.CountOfPallets >= 4)
            {
                //15% discount for equal or more than 4 pallets
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
            await this.db.SaveChangesAsync();

            var viewModel = new ShipmentViewModel
            {
                Id = input.Id,
                From = fromCity.Name,
                To = toCity.Name,
                CountOfPallets = input.CountOfPallets,
                Length = input.Length,
                Width = input.Width,
                Height = input.Height,
                Weight = input.Weight,
                Price = input.Price,
                IsExpressDelivery = input.IsExpressDelivery
            };

            return viewModel;
        }

        public Shipment GetShipmentById(string shipmentId)
        {
            return this.db.Shipments.FirstOrDefault(s => s.Id == shipmentId);
        }
    }
}
