﻿using LogitWebApp.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogitWebApp.ViewModels.Pagination
{
    public class PaginationViewModel
    {
        public IEnumerable<UserOrderViewModel> Orders { get; set; }

        public int PageNumber { get; set; }//2

        public int ItemsPerPage { get; set; }//6

        public int OrdersCount { get; set; }//65

        public int TotalPagesCount => (int)Math.Ceiling((double)this.OrdersCount / this.ItemsPerPage);//11

        public int PreviousPage => this.PageNumber - 1;

        public bool HasPreviousPage => this.PageNumber > 1;

        public int NextPage => this.PageNumber + 1;

        public bool HasNextPage => this.PageNumber < this.TotalPagesCount;





    }
}
