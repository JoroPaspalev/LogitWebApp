using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.Data;

namespace LogitWebApp.Services.DeleteOrder
{
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly ApplicationDbContext db;

        public DeleteOrderService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> DeleteOrderAsync(string orderId)
        {
            var currOrder = this.db.Orders.FirstOrDefault(o => o.Id == orderId);           

            this.db.Orders.Remove(currOrder);
            await this.db.SaveChangesAsync();

            return currOrder != null;
        }

        public bool IsOrderExist(string orderId)
        {
            return this.db.Orders.Any(o => o.Id == orderId) ? true : false;
        }
    }
}
