using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LogitWebApp.Areas.Identity.IdentityHostingStartup))]
namespace LogitWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}