using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Users
{
    public class UserOrderViewModel
    {
        public string OrderId { get; set; }

        public int CountOfPallets { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Description { get; set; }

        public double? Width { get; set; }

        public double? Length { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }

        public string Price { get; set; }

        public string Comment { get; set; }

        public bool IsFragile { get; set; }

        public bool IsExpressDelivery { get; set; }

        public bool IsDelivered { get; set; }

        [DataType(DataType.Date)]
        public DateTime? OrderCreatedOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LoadingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UnloadingDate { get; set; }

        public string LoadingAddress { get; set; }

        public string UnloadingAddress { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string DriverFirstName { get; set; }

        public string DriverLastName { get; set; }
    }
}
