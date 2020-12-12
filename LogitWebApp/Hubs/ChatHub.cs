using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.ChatHub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;


//public class Group
//{
//    public string Name { get; set; }

//    public string Admin_Name { get; set; }

//    public string User_Name { get; set; }
//}

namespace LogitWebApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
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


            await this.Clients.All.SendAsync("NewMessage", new Message
            {
                User = this.Context.User.Identity.Name,
                Text = message,
            }
            );
        }
    }
}
