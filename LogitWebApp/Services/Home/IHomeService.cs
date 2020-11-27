using LogitWebApp.Data.Models;
using System.Collections.Generic;
using LogitWebApp.ViewModels.Offer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LogitWebApp.Services.Home
{
    public interface IHomeService
    {        
        IEnumerable<SelectListItem> GetCities();

        bool IsThereThatCity(int cityId);

        string CreateShipment(OfferInputModel input);
    }
}
