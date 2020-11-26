using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using LogitWebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LogitWebApp.Services.Offers;
using LogitWebApp.Services.Orders;
using LogitWebApp.Services.Drivers;
using LogitWebApp.Services.Home;
using Microsoft.AspNetCore.Mvc;

using LogitWebApp.Data.Seeding;
using LogitWebApp.Data.Models;
using LogitWebApp.Services.Users;
using LogitWebApp.Services.Export;
using WHMS.Services.Common;

namespace LogitWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    //давам го на true ако искам преди да се логне да ми потвърди email-а си
                    options.SignIn.RequireConfirmedAccount = false;

                    //Това е за потвърждение на Email-а
                    options.SignIn.RequireConfirmedEmail = false;

                    //Тези настройки са за паролата. По default са задължителни. Тук ги махам 
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 0;

                    //При колко опита за грешна парола да се заключи акаунта
                    options.Lockout.MaxFailedAccessAttempts = 2;

                    //Изискваш ли в Db да има само един user с този emdil. Ако го дам на false ще може Pesho и Ivan да бъдат с един и същи Email. Което не е много OK
                    options.User.RequireUniqueEmail = true;


                    //Кои символи позволяваме да има в Username
                    //options.User.AllowedUserNameCharacters = "abcdefj... ABCDEFG... 0123456789_-!@#";

                })

                //In your ConfigureServices method, you're already calling AddDefaultIdentity which is a new addition in 2.1 that scaffolds Identity without role support
                //тъй като по default Identity-то на ASP.Net Core не поддържа Роли, трябва да ги добавим ръчно иначе не намира roleManager-а там където трябва да се inject-не и гърми
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews(configure =>
            {
                //Това ми добавя [ValidateAntiForgeryToken] на всеки Action който приема <form> при [HttpPost]
                configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddRazorPages();
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "1283745898662878";
                options.AppSecret = "a4bd3196130530380091c1cb9f617e13";
            });

            services.AddTransient<IOffersService, OffersService>();            
            services.AddTransient<IShortStringService, ShortStringService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IDriversService, DriversService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IHtmlToPdfConverter, HtmlToPdfConverter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.Migrate();

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                //Има 3 варианта за улавяне на грешки

                //1.
                //това ми отваря оня готиния прозорец със сините бутони, когато app-a хвърли грешка
                app.UseDeveloperExceptionPage();

                //2.
                //Когато използваме този middleware и app-а хвърли някъде грешка, този middleware я улавя и извиква в HomeController --> action-a MyErrorAction, той пък ще извика viеw, което се намира в Views/Home/MyErrorAction.cshtml
                //app.UseExceptionHandler("/Home/MyErrorAction");

                //3.
                //Каква е идеята тук на този middleware? Ако възникне грешка се връща Response към Browser-а със статус код 302 т.е да направи Redirect към нов адрес. От къде се взема този нов адрес? Посочваме го тук като параметър --> в случая /Home/StatusCodeError?errorCode={0}, като на мястото на нулата се слага StatusCode на грешката (/Home/StatusCodeError?errorCode=404). Това пък от своя страна извиква в HomeController action StatusCodeError и му подава като аргумент 404. След това action-а извиква същото View и то се връща като отговор на Browser-а
                //3.1
                //app.UseStatusCodePagesWithRedirects("/Home/StatusCodeError?errorCode={0}");

                //Тъй като UseStatusCodePagesWithRedirects не ми хваща грешки от тип 500, трябва да регистрирам и middleware UseExceptionHandler за да може той да ги улавя и връща View за тях

                //Спри 1 и пусни тези трите неща за да ти излезе усмихнатото човече 
                ////////app.UseExceptionHandler("/Home/MyErrorAction");
                //////////3.2
                ////////app.UseStatusCodePagesWithReExecute("/Home/StatusCodeError", "?errorCode={0}");

                ////////app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();//Това е middleware който ми зарежда всички статични файлове. Ако го махна от тук няма да имам .css .js .jpg...
            //Ако искам да сменя default папка wwwroot да не е тя, а да се казва примерно StaticFiles 
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    RequestPath = "/StaticFiles"
            //});
            //и сега вместо да отварям снимката с https://localhost:44314/img/bg_1.jpg ще я отварям с https://localhost:44314/StaticFiles/img/bg_1.jpg

            app.UseRouting();

            //Това да двата middlewares които ми активират атрибутите за Authentication. Трябва да са точно в този ред за да работят
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });            
        }
    }
}
