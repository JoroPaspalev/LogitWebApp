using LogitWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewComponents
{
    public class AllOrderedShipmentsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public AllOrderedShipmentsViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke(string message)
        {
            //Искам да мога в Layout-а да показвам броя на всички направени до момента поръчки
            var viewModel = new ViewModels.ViewComponents.AllOrderedShipmentsViewModel
            {
                Message = message,
                OrdersCount = this.db.Orders.Count()
            };

            return this.View(viewModel);
        }
    }
}
