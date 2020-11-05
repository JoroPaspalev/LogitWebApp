using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
       
        public int? CreatorId { get; set; }
        public Participant Creator { get; set; }
        //public ApplicationUser Creator { get; set; }

        [Required]
        [ForeignKey("Shipment")]
        public string ShipmentId { get; set; }

        public Shipment Shipment { get; set; }
    }
}
