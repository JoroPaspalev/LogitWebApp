using LogitWebApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

//Това казва използвай статичните данни от static class GlobalConstant
using static LogitWebApp.Common.GlobalConstants;

namespace LogitWebApp.Data.Seeding
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new string[] { Admin_RoleName, Driver_RoleName, User_RoleName };

            foreach (var role in roles)
            {
                //Това добавя нова Role с името Admin в таблицата AspNetRoles, ако вече я няма
                IdentityRole currRole = await roleManager.FindByNameAsync(role);
                if (currRole == null)
                {
                    //Ако я няма търсената Роля я направи
                    var identityRole = new IdentityRole()
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    };

                    //Имаме вече роля, сега трябва да я добавим в базата данни, като използваме roleManager
                    await roleManager.CreateAsync(identityRole);
                }
            };            
        }

        public static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
        {
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
            }
        }
    }
}
