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
                    //����� �� �� true ��� ����� ����� �� �� ����� �� �� �������� email-� ��
                    options.SignIn.RequireConfirmedAccount = false;

                    //���� � �� ������������ �� Email-�
                    options.SignIn.RequireConfirmedEmail = false;

                    //���� ��������� �� �� ��������. �� default �� ������������. ��� �� ����� 
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 0;

                    //��� ����� ����� �� ������ ������ �� �� ������� �������
                    options.Lockout.MaxFailedAccessAttempts = 2;

                    //�������� �� � Db �� ��� ���� ���� user � ���� emdil. ��� �� ��� �� false �� ���� Pesho � Ivan �� ����� � ���� � ���� Email. ����� �� � ����� OK
                    options.User.RequireUniqueEmail = true;


                    //��� ������� ����������� �� ��� � Username
                    //options.User.AllowedUserNameCharacters = "abcdefj... ABCDEFG... 0123456789_-!@#";

                })

                //In your ConfigureServices method, you're already calling AddDefaultIdentity which is a new addition in 2.1 that scaffolds Identity without role support
                //��� ���� �� default Identity-�� �� ASP.Net Core �� �������� ����, ������ �� �� ������� ����� ����� �� ������ roleManager-� ��� ������ ������ �� �� inject-�� � �����
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews(configure =>
            {
                //���� �� ������ [ValidateAntiForgeryToken] �� ����� Action ����� ������ <form> ��� [HttpPost]
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
                //��� 3 �������� �� ������� �� ������

                //1.
                //���� �� ������ ��� ������� �������� ��� ������ ������, ������ app-a ������ ������
                app.UseDeveloperExceptionPage();

                //2.
                //������ ���������� ���� middleware � app-� ������ ������ ������, ���� middleware � ����� � ������� � HomeController --> action-a MyErrorAction, ��� ��� �� ������ vi�w, ����� �� ������ � Views/Home/MyErrorAction.cshtml
                //app.UseExceptionHandler("/Home/MyErrorAction");

                //3.
                //����� � ������ ��� �� ���� middleware? ��� �������� ������ �� ����� Response ��� Browser-� ��� ������ ��� 302 �.� �� ������� Redirect ��� ��� �����. �� ���� �� ����� ���� ��� �����? ��������� �� ��� ���� ��������� --> � ������ /Home/StatusCodeError?errorCode={0}, ���� �� ������� �� ������ �� ����� StatusCode �� �������� (/Home/StatusCodeError?errorCode=404). ���� ��� �� ���� ������ ������� � HomeController action StatusCodeError � �� ������ ���� �������� 404. ���� ���� action-� ������� ������ View � �� �� ����� ���� ������� �� Browser-�
                //3.1
                //app.UseStatusCodePagesWithRedirects("/Home/StatusCodeError?errorCode={0}");

                //��� ���� UseStatusCodePagesWithRedirects �� �� ����� ������ �� ��� 500, ������ �� ����������� � middleware UseExceptionHandler �� �� ���� ��� �� �� ����� � ����� View �� ���

                //���� 1 � ����� ���� ����� ���� �� �� �� ������ ����������� ������ 
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
            app.UseStaticFiles();//���� � middleware ����� �� ������� ������ �������� �������. ��� �� ����� �� ��� ���� �� ���� .css .js .jpg...
            //��� ����� �� ����� default ����� wwwroot �� �� � ��, � �� �� ����� �������� StaticFiles 
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    RequestPath = "/StaticFiles"
            //});
            //� ���� ������ �� ������� �������� � https://localhost:44314/img/bg_1.jpg �� � ������� � https://localhost:44314/StaticFiles/img/bg_1.jpg

            app.UseRouting();

            //���� �� ����� middlewares ����� �� ��������� ���������� �� Authentication. ������ �� �� ����� � ���� ��� �� �� �������
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
