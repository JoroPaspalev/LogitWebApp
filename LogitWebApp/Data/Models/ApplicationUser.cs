using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LogitWebApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.DriverVotes = new HashSet<Vote>();
        }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string VatNumber { get; set; }

        public int Bulstat { get; set; }

        public string Manager { get; set; }

        public string Site { get; set; }

        public long? Fax { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Vote> DriverVotes { get; set; }
    }
}
