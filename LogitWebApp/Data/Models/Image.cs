using System;
using System.ComponentModel.DataAnnotations.Schema;
using LogitWebApp.Data.CommonModels;

namespace LogitWebApp.Data.Models
{
    public class Image: BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Extension { get; set; }

        [ForeignKey("Shipment")]
        public string ShipmentId { get; set; }

        public virtual Shipment Shipment { get; set; }

        [ForeignKey("AddedByDriver")]
        public string AddedByDriverId { get; set; }

        public virtual ApplicationUser AddedByDriver { get; set; }

        public string ImageUrl { get; set; }
    }
}
