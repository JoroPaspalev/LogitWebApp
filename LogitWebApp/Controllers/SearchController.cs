using LogitWebApp.Common;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Search;
using LogitWebApp.Services.Users;
using LogitWebApp.ViewModels.Pagination;
using LogitWebApp.ViewModels.Search;
using LogitWebApp.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class SearchController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISearchService searchService;

        public SearchController(
            UserManager<ApplicationUser> userManager,
            ISearchService searchService)
        {
            this.userManager = userManager;
            this.searchService = searchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Result(SearchInputModel input)
        {
            var userId = this.userManager.GetUserId(User);

            ICollection<UserOrderViewModel> filteredOrders = this.searchService.GetAllUserOrders(userId, input);

            return View(filteredOrders);
        }
    }
}
