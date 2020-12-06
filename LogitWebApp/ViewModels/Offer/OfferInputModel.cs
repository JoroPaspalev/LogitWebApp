using LogitWebApp.Attributes.ModelValidationAttributes;
using LogitWebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Offer
{
    public class OfferInputModel
    {

        //[BindNever] - Това означава да не се Binder-ва (mapp-ва) стойност за това пропърти. Трябва много да се внимава, защото ако това пропърти, както в момента е [Required], му сложа [BindNever] означава че няма да му търси стойност и ще остане default-ната му, което ще гръмне при проверката за [Required]
        [Required(ErrorMessage ="Изберете един от посочените градове!")]
        [CheckCityExistAttribute]
        public int From { get; set; }

        [Required(ErrorMessage = "Изберете един от посочените градове!")]
        [CheckCityExistAttribute]
        public int To { get; set; }

        [Required]
        [Range(1, 30, ErrorMessage = "Броя на превозваните палети трябва да е между 1 и 30бр.")]
        public int CountOfPallets { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето Ширина на палета не може да бъде празно!")]
        [Range(0.8, 3.00, ErrorMessage = "Ширината на палета трябва да е между 0.8 и 3.00м!")]
        public double? Width { get; set; }

        [Required(ErrorMessage = "Полето Дължина на палета не може да бъде празно!")]
        [Range(0.8, 2.40, ErrorMessage = "Дължината на палета трябва да е между 0.8 и 2.40м!")]
        public double? Length { get; set; }

        [Required(ErrorMessage = "Полето Височина на палета не може да бъде празно!")]
        [Range(0.10, 2.10, ErrorMessage = "Височината на палета не трябва да е по-ниска от 10см или да надвишава 2.10м!")]
        public double? Height { get; set; }

        [Required(ErrorMessage = "Полето Тегло на палета не може да бъде празно!")]
        [Range(0.1, 1000, ErrorMessage = "Теглото на палета не трябва да е по-малко от 1кг или да надвишава 1000кг")]
        public double? Weight { get; set; }

        public bool IsExpressDelivery { get; set; }

        public bool IsFragile { get; set; }

        public IEnumerable<SelectListItem> Cities{ get; set; }
    }
}
