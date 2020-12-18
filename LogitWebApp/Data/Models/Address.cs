using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Data.Models
{
    public class Address
    {
        public Address()
        {
            this.ShipmentsLoading = new HashSet<Shipment>();
            this.ShipmentsUnloading = new HashSet<Shipment>();
        }

        public int Id { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string StreetNumber { get; set; }

        public string Block { get; set; }

        public int? Floor { get; set; }

        public string Entrance { get; set; }

        public virtual ICollection<Shipment> ShipmentsLoading { get; set; }

        public virtual ICollection<Shipment> ShipmentsUnloading { get; set; }

        public override string ToString()
        {
            return $"{Town}, ул. {Street} N{StreetNumber}";
        }
    }
}
