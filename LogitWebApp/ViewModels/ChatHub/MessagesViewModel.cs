using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.ChatHub
{
    public class MessagesViewModel
    {
        public string Text { get; set; }

        public string OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsRead { get; set; }

        public string userWhoSendMessage { get; set; }
    }
}
