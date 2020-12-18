using System.Threading.Tasks;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Offer;

namespace LogitWebApp.Services.Offers
{
    public interface IOffersService
    {
        public Task<ShipmentViewModel> GetOffer(Shipment input);

        public Shipment GetShipmentById(string shipmentId);
    }
}
