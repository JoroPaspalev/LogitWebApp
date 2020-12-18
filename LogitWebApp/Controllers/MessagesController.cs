using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LogitWebApp.Services.Messages;

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
