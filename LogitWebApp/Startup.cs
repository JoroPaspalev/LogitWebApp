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
using LogitWebApp.Services.SeedDb;
using LogitWebApp.Services.Orders;
using LogitWebApp.Services.Drivers;

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

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)//давам го на true ако искам преди да се логне да ми потвърди email-а си

                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<IOffersService, OffersService>();
            services.AddTransient<ISeedService, SeedService>();
            services.AddTransient<IShortStringService, ShortStringService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IDriversService, DriversService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //Има 3 варианта за улавяне на грешки

                //1.
                //това ми отваря оня готиния прозорец със сините бутони, когато app-a хвърли грешка
                //app.UseDeveloperExceptionPage();

                //2.
                //Когато използваме този middleware и app-а хвърли някъде грешка, този middleware я улавя и извиква в HomeController --> action-a MyErrorAction, той пък ще извика viеw, което се намира в Views/Home/MyErrorAction.cshtml
                //app.UseExceptionHandler("/Home/MyErrorAction");

                //3.
                //Каква е идеята тук на този middleware? Ако възникне грешка се връща Response към Browser-а със статус код 302 т.е да направи Redirect към нов адрес. От къде се взема този нов адрес? Посочваме го тук като параметър --> в случая /Home/StatusCodeError?errorCode={0}, като на мястото на нулата се слага StatusCode на грешката (/Home/StatusCodeError?errorCode=404). Това пък от своя страна извиква в HomeController action StatusCodeError и му подава като аргумент 404. След това action-а извиква същото View и то се връща като отговор на Browser-а
                //3.1
                //app.UseStatusCodePagesWithRedirects("/Home/StatusCodeError?errorCode={0}");

                //Тъй като UseStatusCodePagesWithRedirects не ми хваща грешки от тип 500, трябва да регистрирам и middleware UseExceptionHandler за да може той да ги улавя и връща View за тях
                app.UseExceptionHandler("/Home/MyErrorAction");
                //3.2
                app.UseStatusCodePagesWithReExecute("/Home/StatusCodeError", "?errorCode={0}");

                app.UseDatabaseErrorPage();
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
