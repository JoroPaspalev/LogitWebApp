using LogitWebApp.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Users
{
    public class OrdersPaginationViewModel : PaginationViewModel
    {
        public IEnumerable<UserOrderViewModel> Orders { get; set; }
    }
}
