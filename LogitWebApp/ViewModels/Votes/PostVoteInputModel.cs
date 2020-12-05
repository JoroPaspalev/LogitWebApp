using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Votes
{
    public class PostVoteInputModel
    {
        [Required]
        public string DriverId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
