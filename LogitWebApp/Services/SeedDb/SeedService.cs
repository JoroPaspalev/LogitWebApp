using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.SeedDb
{
    public class SeedService : ISeedService
    {
        private readonly ApplicationDbContext db;

        public SeedService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void SeedDb()
        {
            string inputData = File.ReadAllText("wwwroot/lib/InitialSeedData/FromSofiaTo.txt");

            var splittedData = inputData.Split(new string[] { "\r","\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < splittedData.Length; i+=2)
            {
                var toCity = splittedData[i];
                double km = double.Parse(splittedData[i + 1]);

                string fromCity = "София";//Replace this with new one city - Русе, Варна...

                var currDistance = new Distance()
                {
                    FromCity = fromCity,
                    ToCity = toCity,
                    DistanceInKM = km
                };

                this.db.Add(currDistance);                
            }

            this.db.SaveChanges();

        }
    }
}
