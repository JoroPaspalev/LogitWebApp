using LogitWebApp.ViewModels.Pagination;
using LogitWebApp.ViewModels.Users;

namespace LogitWebApp.Services.Users
{
    public interface IUsersService
    {
        PaginationViewModel GetAllUserOrders(string userId, int id, int items = 6);

        UserOrderViewModel GetOrder(string orderId);

      
    }
}
