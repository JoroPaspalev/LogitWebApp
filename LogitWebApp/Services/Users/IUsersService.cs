using LogitWebApp.ViewModels.Pagination;
using LogitWebApp.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Users
{
    public interface IUsersService
    {
        PaginationViewModel GetAllUserOrders(string userId, int id, int items = 6);

        UserOrderViewModel GetOrder(string orderId);

        UserOrderViewModel GetImages(string orderId);
    }
}
