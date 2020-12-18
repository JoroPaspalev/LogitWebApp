
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogitWebApp.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Messages = new HashSet<Message>();
        }

        [Key]
        public string Id { get; set; }
       
        [Required]
        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        [Required]
        [ForeignKey("Shipment")]
        public string ShipmentId { get; set; }

        public Shipment Shipment { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
