using LogitWebApp.Services.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class MessagesController : Controller
    {
        private readonly IMessagesService messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        public IActionResult AllUnreadMessages()
        {
            var unreadMessages = this.messagesService.GetAllUnreadMessages();

            return this.View(unreadMessages);
        }
    }
}
