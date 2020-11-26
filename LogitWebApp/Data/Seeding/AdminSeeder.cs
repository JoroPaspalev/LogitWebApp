using System;
using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static LogitWebApp.Common.GlobalConstants;

namespace LogitWebApp.Data.Seeding
{
    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //Сега ми намери user-а, който е с този email. Ако няма такъв, значи базата е изтрита и трябва да си създам нов user, който в последствие да го добавя в роля = "Admin"
            ApplicationUser user = await userManager.FindByEmailAsync(Admin_Email);
            if (user == null)
            {
                //Ако резултата от търсенето е 0, то направи този user
                ApplicationUser appUser = new ApplicationUser()
                {
                    UserName = Admin_Email,
                    Email = Admin_Email,
                    PhoneNumber = Admin_Phone,
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    AccessFailedCount = 0,
                };

                //Сега този направения user го дай на userManager-а, задай и парола, която той ще Hash-ира и после ще запише всички данни на този юзър в базата данни
                IdentityResult result = await userManager.CreateAsync(appUser, Admin_Pass);

                //След като успешно сме създали пърло - Роля, второ - User, сега трябва да добавим този новосъздаден User в mapping Table UserRoles с неговата роля!!! Това го прави userManager-a, а не RoleManager-а.(RoleManager-а прави само Roles, не е негова работа да добавя user в Roles!)
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, Admin_RoleName);
                }

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
