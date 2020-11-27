using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Offer
{
    public class ShipmentViewModel
    {
        public string Id { get; set; }

        public int CountOfPallets { get; set; }

        public string From { get; set; }

        public string To { get; set; }
        
        public double? Width { get; set; }

        public double? Length { get; set; }

        public double? Height { get; set; }
       
        public double? Weight { get; set; }

        public decimal Price { get; set; }

        public bool IsExpressDelivery { get; set; }
    }
}
