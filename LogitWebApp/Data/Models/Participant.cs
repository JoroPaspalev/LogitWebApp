using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Models
{
    public class Participant
    {
        public Participant()
        {
            this.ShipmentSenders = new HashSet<Shipment>();
            this.ShipmentReceivers = new HashSet<Shipment>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{4}-[0-9]{3}-[0-9]{3}")]
        public string Phone { get; set; }

        [MaxLength(45)]
        public string Email { get; set; }

        public virtual ICollection<Shipment> ShipmentSenders { get; set; }

        public virtual ICollection<Shipment> ShipmentReceivers { get; set; }

        //[Required]
        //[ForeignKey("Address")]
        //public int AddressId { get; set; }

        //public virtual Address Address { get; set; }
    }
}
