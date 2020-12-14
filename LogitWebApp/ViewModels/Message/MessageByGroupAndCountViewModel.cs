using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.ViewModels.Message
{
    public class MessageByGroupAndCountViewModel
    {
        public string OrderId { get; set; }

        public int CountUnreadMessages { get; set; }
    }
}
