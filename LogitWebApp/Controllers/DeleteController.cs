using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LogitWebApp.Services.DeleteOrder;
using LogitWebApp.ViewModels.Delete;
using LogitWebApp.ViewModels.Shared;
using static LogitWebApp.Common.GlobalConstants;

namespace LogitWebApp.Controllers
{
    [Authorize(Roles = Admin_RoleName)]
    public class DeleteController : Controller
    {
        private readonly IDeleteOrderService deleteOrderService;

        public DeleteController(IDeleteOrderService deleteOrderService)
        {
            this.deleteOrderService = deleteOrderService;
        }

        public IActionResult DeleteOrder()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(DeleteOrderInputModel input)
        {
            if (!this.deleteOrderService.IsOrderExist(input.OrderId))
            {
                ModelState.AddModelError("OrderId", "Поръчка с този номер не съществува!");
            }

            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.deleteOrderService.DeleteOrderAsync(input.OrderId);

            return this.RedirectToAction("OrderDeleted", "Delete", new ChangesApplied { Message = Order_Deleted });
        }

        public IActionResult OrderDeleted(ChangesApplied input)
        {
            return this.View(input);
        }
    }
}
