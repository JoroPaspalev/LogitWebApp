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

        [Required(ErrorMessage ="Задължително поле")]
        public double? Width { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        public double? Length { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        public double? Height { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        public double? Weight { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        public int CountOfPallets { get; set; }

        public string Comment { get; set; }

        public bool IsDelivered { get; set; }

    }
}
