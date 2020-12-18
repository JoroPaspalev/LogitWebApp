using System.Collections.Generic;
using LogitWebApp.ViewModels.Message;

namespace LogitWebApp.Services.Messages
{
    public interface IMessagesService
    {
        ICollection<MessageByGroupAndCountViewModel> GetAllUnreadMessages();
    }
}
