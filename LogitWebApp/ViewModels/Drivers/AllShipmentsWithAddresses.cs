using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Drivers
{
    public class AllShipmentsWithAddresses
    {
        public string Id { get; set; }

        public int CountOfPallets { get; set; }

        public string Description { get; set; }
       
        public double? Width { get; set; }

        public double? Length { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }

        public string Comment { get; set; }

        public string IsFragile { get; set; }        

        public string LoadingDate { get; set; }

        public string UnloadingDate { get; set; }

        public string LoadingAddress { get; set; }

        public string UnloadingAddress { get; set; }


    }
}
