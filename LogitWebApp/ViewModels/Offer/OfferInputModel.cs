using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Offer
{
    public class OfferInputModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public int CountOfPallets { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public DateTime LoadingDay { get; set; }

        public DateTime UnloadingDay { get; set; }

        public string IsExpressDelivery { get; set; } //Is Express delivery or not

        public string IsFragile { get; set; }
    }
}
