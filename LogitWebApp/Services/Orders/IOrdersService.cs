using System.Collections.Generic;
using LogitWebApp.ViewModels.ChatHub;
using LogitWebApp.ViewModels.Offer;

namespace LogitWebApp.Services.Orders
{
    public interface IOrdersService
    {
        string CreateOrder(AddressInputModel input, string userId);

        ICollection<MessagesViewModel> GetAllMessages(string orderId);

        ICollection<MessagesViewModel> GetAllNotReadFromAdminMessages();

        void ChangeAllUnreadMessagesWithThisOrderId(string orderId);
    }
}
