using LogitWebApp.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Home
{
    public interface IHomeService
    {
        public string CreateShipment(OfferInputModel input);
    }
}
