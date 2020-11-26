using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static LogitWebApp.Common.GlobalConstants;

namespace LogitWebApp.Data.Seeding
{
    public class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRoleAsync(roleManager);
        }
        private static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new string[] { Admin_RoleName, Driver_RoleName, User_RoleName };
            foreach (var role in roles)
            {
                //Това добавя нова Role с името Admin, User или Driver в таблицата AspNetRoles, ако вече я няма
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
                    var result = await roleManager.CreateAsync(identityRole);

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
