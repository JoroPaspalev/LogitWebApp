using LogitWebApp.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Users
{
    public interface IUsersService
    {
        public IEnumerable<UserOrderViewModel> GetAllUserOrders(string userId);

        public UserOrderViewModel GetOrder(string orderId);
    }
}
