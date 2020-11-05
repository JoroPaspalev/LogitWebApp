using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Models
{
    public class Distance
    {
        public int Id { get; set; }

        public string FromCity { get; set; }

        public string ToCity { get; set; }

        public double DistanceInKM { get; set; }

    }
}
