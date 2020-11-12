using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Models
{
    public class Driver
    {
        public Driver()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{4}-[0-9]{3}-[0-9]{3}")]
        public string PhoneNumber { get; set; }
    }
}
