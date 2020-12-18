using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using LogitWebApp.ViewModels.Offer;

namespace LogitWebApp.Services.Home
{
    public interface IHomeService
    {        
        IEnumerable<SelectListItem> GetCities();

        bool IsThereThatCity(int cityId);

        string CreateShipment(OfferInputModel input);
    }
}
