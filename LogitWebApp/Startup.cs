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

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)//����� �� �� true ��� ����� ����� �� �� ����� �� �� �������� email-� ��

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
                //���� �� ������ ��� ������� �������� ��� ������ ������, ������ app-a ������ ������
                //app.UseDeveloperExceptionPage();

                //������ ���������� ���� middleware � app-� ������ ������ ������, ���� middleware � ����� � ������� � HomeController --> action-a MyErrorAction, ��� ��� �� ������ vi�w, ����� �� ������ � Views/Home/MyErrorAction.cshtml
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
            app.UseStaticFiles();//���� � middleware ����� �� ������� ������ �������� �������. ��� �� ����� �� ��� ���� �� ���� .css .js .jpg...
            //��� ����� �� ����� default ����� wwwroot �� �� � ��, � �� �� ����� �������� StaticFiles 
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    RequestPath = "/StaticFiles"
            //});
            //� ���� ������ �� ������� �������� � https://localhost:44314/img/bg_1.jpg �� � ������� � https://localhost:44314/StaticFiles/img/bg_1.jpg

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
