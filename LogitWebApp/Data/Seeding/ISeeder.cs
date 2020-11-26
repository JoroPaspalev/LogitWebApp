using System;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Seeding
{
    public interface ISeeder
    {
        public Task SeedAsync(ApplicationDbContext db, IServiceProvider serviceProvider);
    }
}
