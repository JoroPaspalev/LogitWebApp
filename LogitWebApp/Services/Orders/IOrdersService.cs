using LogitWebApp.ViewModels.ChatHub;
using LogitWebApp.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
