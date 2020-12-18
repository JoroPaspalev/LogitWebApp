using System.Linq;
using LogitWebApp.ViewModels.Search;
using LogitWebApp.ViewModels.Users;

namespace LogitWebApp.Services.Search
{
    public interface ISearchService
    {
        IQueryable<UserOrderViewModel> GetAllUserOrders(string userId, SearchInputModel input);
    }
}
