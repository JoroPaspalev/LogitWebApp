using LogitWebApp.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Drivers
{
    public class ShipmentsPaginationViewModel : PaginationViewModel
    {
        public IEnumerable<AllShipmentsWithAddresses> ShipmentsOfCurrPage { get; set; }
    }
}
