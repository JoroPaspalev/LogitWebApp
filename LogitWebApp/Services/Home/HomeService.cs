using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Offer;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IEnumerable<SelectListItem> GetCities()
        {            
            var selectListItems = this.db.Cities.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            .OrderBy(x => x.Text)
            .ToList();

            return selectListItems;
        }
                
        public string CreateShipment(OfferInputModel input)
        {
            var currShipment = new Shipment()
            {
                FromCityId = input.From,
                ToCityId = input.To,
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

        public bool IsThereThatCity(int cityId)
        {
            var currCity = this.db.Cities.FirstOrDefault(c => c.Id == cityId);

            return currCity == null ? false : true;
        }
    }
}
