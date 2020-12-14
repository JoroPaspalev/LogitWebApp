using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.ChatHub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using LogitWebApp.Common;
using System.Security.Claims;
using System;
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


            //string adminUsername = "joro_thexfiles@abv.bg";

            //List<Group> groups = new List<Group>();

            //var currUser = this.Context.User;

            //if (currUser.Identity.Name != adminUsername)
            //{
            //    var currGroup = new Group
            //    {
            //        Name = currUser.Identity.Name + adminUsername,
            //        User_Name = currUser.Identity.Name,
            //        Admin_Name = adminUsername
            //    };

            //    groups.Add(currGroup);
            //}

            //var group = groups.FirstOrDefault(g => g.Name == currUser.Identity.Name + adminUsername);

            //if (group != null)
            //{
            //    this.Clients.
            //}

            //if (currUser.Identity.Name == "joro_thexfiles@abv.bg")
            //{
            //    //направи група и го вкарай в нея
            //    await this.Groups.AddToGroupAsync(Context.ConnectionId, "Admin_with_user");
            //}
            //else
            //{
            //    //Ако броя на хората в група Admin_with_user е 2 тогава направи нова група и вкарай Admin в нея

            //    await this.Groups.AddToGroupAsync(Context.ConnectionId, "Admin_with_user");
            //}


            await this.Clients.All.SendAsync("NewMessage", new MessageViewModel
            {
                User = this.Context.User.Identity.Name,
                Text = message,
            }
            );
        }
    }
}
