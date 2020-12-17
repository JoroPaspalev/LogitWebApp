using System.Threading.Tasks;

namespace LogitWebApp.Services.DeleteOrder
{
    public interface IDeleteOrderService
    {
        bool IsOrderExist(string orderId);

        Task<bool> DeleteOrderAsync(string orderId);
    }
}
