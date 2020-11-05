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
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)//давам го на true ако искам преди да се логне да ми потвърди email-а си

                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<IOffersService, OffersService>();
            services.AddTransient<ISeedService, SeedService>();
            services.AddTransient<IShortStringService, ShortStringService>();
            services.AddTransient<IOrdersService, OrdersService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Remove may be from here
            new ApplicationDbContext().Database.Migrate();

            if (env.IsDevelopment())
            {
                //това ми отваря оня готиния прозорец със сините бутони, когато app-a хвърли грешка
                //app.UseDeveloperExceptionPage();

                //Когато използваме този middleware и app-а хвърли някъде грешка, този middleware я улавя и извиква в HomeController --> action-a MyErrorAction, той пък ще извика viеw, което се намира в Views/Home/MyErrorAction.cshtml
                app.UseExceptionHandler("/Home/MyErrorAction");
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
