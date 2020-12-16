using LogitWebApp.ViewModels.Search;
using LogitWebApp.ViewModels.Users;
using System.Collections.Generic;

namespace LogitWebApp.Services.Search
{
    public interface ISearchService
    {
        ICollection<UserOrderViewModel> GetAllUserOrders(string userId, SearchInputModel input);
    }
}
