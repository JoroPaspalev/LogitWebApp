using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Seeding
{
    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db, IServiceProvider serviceProvider)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));

            var seeders = new List<ISeeder>()
            {
                new RolesSeeder(),
                new AdminSeeder(),
                new DistancesSeeder(),
                new CitiesSeeder(),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(db, serviceProvider);
                await db.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
