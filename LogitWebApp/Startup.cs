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

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)//����� �� �� true ��� ����� ����� �� �� ����� �� �� �������� email-� ��

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
                //��� 3 �������� �� ������� �� ������

                //1.
                //���� �� ������ ��� ������� �������� ��� ������ ������, ������ app-a ������ ������
                //app.UseDeveloperExceptionPage();

                //2.
                //������ ���������� ���� middleware � app-� ������ ������ ������, ���� middleware � ����� � ������� � HomeController --> action-a MyErrorAction, ��� ��� �� ������ vi�w, ����� �� ������ � Views/Home/MyErrorAction.cshtml
                //app.UseExceptionHandler("/Home/MyErrorAction");

                //3.
                //����� � ������ ��� �� ���� middleware? ��� �������� ������ �� ����� Response ��� Browser-� ��� ������ ��� 302 �.� �� ������� Redirect ��� ��� �����. �� ���� �� ����� ���� ��� �����? ��������� �� ��� ���� ��������� --> � ������ /Home/StatusCodeError?errorCode={0}, ���� �� ������� �� ������ �� ����� StatusCode �� �������� (/Home/StatusCodeError?errorCode=404). ���� ��� �� ���� ������ ������� � HomeController action StatusCodeError � �� ������ ���� �������� 404. ���� ���� action-� ������� ������ View � �� �� ����� ���� ������� �� Browser-�
                //3.1
                //app.UseStatusCodePagesWithRedirects("/Home/StatusCodeError?errorCode={0}");

                //��� ���� UseStatusCodePagesWithRedirects �� �� ����� ������ �� ��� 500, ������ �� ����������� � middleware UseExceptionHandler �� �� ���� ��� �� �� ����� � ����� View �� ���
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
