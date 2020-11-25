using Microsoft.AspNetCore.Http;
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

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public double? Width { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public double? Length { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public double? Height { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public double? Weight { get; set; }

        [Display(Name = "Коментар")]
        public string Comment { get; set; }

        public string IsFragile { get; set; }

        public bool IsDelivered { get; set; }

        public DateTime LoadingDate { get; set; }

        public DateTime UnloadingDate { get; set; }

        [Display(Name = "Адрес на товарене")]
        [Required]
        public string LoadingAddress { get; set; }

        [Display(Name = "Адрес на разтоварене")]
        [Required]
        public string UnloadingAddress { get; set; }

        [Display(Name = "Прикачи снимка")]
        public IFormFile Picture { get; set; }
    }
}
