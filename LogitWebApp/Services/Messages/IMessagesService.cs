using LogitWebApp.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Messages
{
    public interface IMessagesService
    {
        ICollection<MessageByGroupAndCountViewModel> GetAllUnreadMessages();
    }
}
