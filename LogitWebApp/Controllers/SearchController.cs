﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Search;
using LogitWebApp.ViewModels.Search;
using LogitWebApp.ViewModels.Users;

namespace LogitWebApp.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class SearchController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISearchService searchService;

        public SearchController(UserManager<ApplicationUser> userManager, ISearchService searchService)
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

            IQueryable<UserOrderViewModel> filteredOrders = this.searchService.GetAllUserOrders(userId, input);

            return View(filteredOrders);
        }
    }
}
