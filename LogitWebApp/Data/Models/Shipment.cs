using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Models
{
    public class Shipment
    {
        public Shipment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public int CountOfPallets { get; set; }

        [Required]
        [MaxLength(20)]
        public string From { get; set; }

        [Required]
        [MaxLength(20)]
        public string To { get; set; }

        public string Description { get; set; }

        [Required]
        public double? Width { get; set; }

        [Required]
        public double? Length { get; set; }

        [Required]
        public double? Height { get; set; }

        [Required]
        public double? Weight { get; set; }

        public decimal Price { get; set; }

        public string Comment { get; set; }

        public bool IsFragile { get; set; }

        public bool IsExpressDelivery { get; set; }

        public bool IsDelivered { get; set; }

        public DateTime? OrderCreatedOn { get; set; }

        public DateTime? LoadingDate { get; set; }

        public DateTime? UnloadingDate { get; set; }       

        
        public int? LoadingAddressId { get; set; }
        public virtual Address LoadingAddress { get; set; }

        
        public int? UnloadingAddressId { get; set; }
        public virtual Address UnloadingAddress { get; set; }

        public int? SenderId { get; set; }
        public virtual Participant Sender { get; set; }

        public int? ReceiverId { get; set; }
        public virtual Participant Receiver { get; set; }

        //public int PayerId { get; set; }
        //public virtual Participant Payer { get; set; }

        public int? DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
