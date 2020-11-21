using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string VatNumber { get; set; }

        public int Bulstat { get; set; }

        public string Manager { get; set; }

        public string Site { get; set; }

        public long? Fax { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
