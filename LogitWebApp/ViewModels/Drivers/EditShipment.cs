using LogitWebApp.Attributes.ModelValidationAttributes;
using LogitWebApp.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Drivers
{
    public class EditShipment
    {
        [Required(ErrorMessage = "Задължително поле")]
        public string ShipmentId { get; set; }

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

        [Required(ErrorMessage = "Броя на палетите e задължително поле")]
        [Range(1, 30, ErrorMessage = "Броя на палетите е между 1 и 30")]
        public int? CountOfPallets { get; set; }

        public string Comment { get; set; }

        public bool IsDelivered { get; set; }

        [Accept_Only_jpg_png]
        [ValidateImageSize(2)]
        public IFormFile Picture { get; set; }

        public Image Image { get; set; }
    }
}
