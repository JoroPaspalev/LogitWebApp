using System;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.ChatHub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using LogitWebApp.Common;
using System.Security.Claims;
using LogitWebApp.Data;

namespace LogitWebApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext db;

        public ChatHub(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Send(string message, string orderId)
        {
            bool isAdmin = this.Context.User.IsInRole(GlobalConstants.Admin_RoleName);
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currMessage = new Message
            {
                IsAdmin = isAdmin,
                CreatedOn = DateTime.UtcNow,
                OrderId = orderId,
                Text = message
            };

            await this.db.Messages.AddAsync(currMessage);
            await this.db.SaveChangesAsync();

            await this.Clients.OthersInGroup(orderId)
                .SendAsync("NewMessage", message);

            await this.Clients.All.SendAsync("NewMessage", new MessageViewModel
            {
                User = this.Context.User.Identity.Name,
                Text = message,
            }
            );
        }
    }
}
