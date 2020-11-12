using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Drivers
{
    public class AllShipmentsWithAddresses
    {
        public string Id { get; set; }

        [Display(Name = "Брой палети")]
        public int CountOfPallets { get; set; }

        public string Description { get; set; }

        public double? Width { get; set; }

        public double? Length { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }

        [Display(Name = "Коментар")]
        public string Comment { get; set; }

        public string IsFragile { get; set; }

        public bool IsDelivered { get; set; }

        public DateTime LoadingDate { get; set; }

        public DateTime UnloadingDate { get; set; }

        [Display(Name = "Адрес на товарене")]
        public string LoadingAddress { get; set; }

        [Display(Name = "Адрес на разтоварене")]
        public string UnloadingAddress { get; set; }


    }
}
