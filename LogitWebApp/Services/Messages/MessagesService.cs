using System.Linq;
using System.Collections.Generic;
using LogitWebApp.Data;
using LogitWebApp.Services.Orders;
using LogitWebApp.ViewModels.Message;

namespace LogitWebApp.Services.Messages
{
    public class MessagesService : IMessagesService
    {
        private readonly ApplicationDbContext db;
        private readonly IOrdersService ordersService;

        public MessagesService(ApplicationDbContext db, IOrdersService ordersService)
        {
            this.db = db;
            this.ordersService = ordersService;
        }

        public ICollection<MessageByGroupAndCountViewModel> GetAllUnreadMessages()
        {
            var unreadMessages = this.ordersService.GetAllNotReadFromAdminMessages();

            return unreadMessages.
                GroupBy(x => x.OrderId)
                .Select(x => new MessageByGroupAndCountViewModel
                {
                    OrderId = x.Key,
                    CountUnreadMessages = x.Select(y=>y).Count()
                })
                .ToList();
        }
    }
}
