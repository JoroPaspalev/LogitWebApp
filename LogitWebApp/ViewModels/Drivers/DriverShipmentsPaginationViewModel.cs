using LogitWebApp.ViewModels.Pagination;
using System.Collections.Generic;

namespace LogitWebApp.ViewModels.Drivers
{
    public class DriverShipmentsPaginationViewModel : PaginationViewModel
    {
        public IEnumerable<AllShipmentsWithAddresses> DriverShipmentsOfCurrentPage { get; set; }
    }
}
