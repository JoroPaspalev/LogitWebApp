using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Offers
{
    public interface IOffersService
    {
        public Shipment GetOffer(OfferInputModel input);
    }
}
