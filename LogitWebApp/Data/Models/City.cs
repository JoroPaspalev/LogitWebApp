using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogitWebApp.Data.Models
{
    public class City
    {
        public City()
        {
            this.ShipmentsFromThisCity = new HashSet<Shipment>();
            this.ShipmentsToThisCity = new HashSet<Shipment>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Shipment> ShipmentsFromThisCity { get; set; }

        public IEnumerable<Shipment> ShipmentsToThisCity { get; set; }
    }
}
