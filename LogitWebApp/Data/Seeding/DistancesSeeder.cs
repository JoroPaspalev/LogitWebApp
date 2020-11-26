using LogitWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Data.Seeding
{
    public class DistancesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db, IServiceProvider serviceProvider)
        {
            bool isThereAnyDistanceInDb = db.Distances.Any();

            if (isThereAnyDistanceInDb)
            {
                return;
            }

            List<string> cityNamesBG = new List<string>()
            {
                "София"
                ,
                "Пловдив"
                ,
                "Варна"
                ,
                "Бургас"
                ,
                "Стара Загора"
                ,
                "Русе"
                ,
                "Шумен"
                ,
                "Плевен"
                ,
                "Велико Търново"
                ,
                "Благоевград"
                ,
                "Сливен"
                ,
                "Ямбол"
                ,
                "Видин"
                ,
                "Враца"
                ,
                "Добрич"
                ,
                "Габрово"
                ,
                "Перник"
                ,
                "Хасково"
                ,
                "Търговище"
                ,
                "Смолян"
                ,
                "Силистра"
                ,
                "Разград"
                ,
                "Пазарджик"
                ,
                "Монтана"
                ,
                "Ловеч"
                ,
                "Кюстендил"
                ,
                "Кърджали"
            };

            List<string> cityNamesEN = new List<string>()
            {
                "Sofia"
                ,
                "Plovdiv"
                ,
                "Varna"
                ,
                "Burgas"
                ,
                "StaraZagora"
                ,
                "Ruse"
                ,
                "Shumen"
                ,
                "Pleven"
                ,
                "VelikoTurnovo"
                ,
                "BlagoevGrad"
                ,
                "Sliven"
                ,
                "Yambol"
                ,
                "Vidin"
                ,
                "Vratsa"
                ,
                "Dobrich"
                ,
                "Gabrovo"
                ,
                "Pernik"
                ,
                "Haskovo"
                ,
                "Turgovishte"
                ,
                "Smolyan"
                ,
                "Silistra"
                ,
                "Razgrad"
                ,
                "Pazardjik"
                ,
                "Montana"
                ,
                "Lovetch"
                ,
                "Kyustendil"
                ,
                "Kurdjali"
            };

            Dictionary<string, string> cityNames = new Dictionary<string, string>();

            //В момента има попълнени само 5 файла с данни за разстоянията между градовете затова е i < 5
            for (int i = 0; i < 5; i++)
            {
                cityNames.Add(cityNamesBG[i], cityNamesEN[i]);
            }

            foreach (var kvp in cityNames)
            {
                string bgName = kvp.Key;
                string enName = kvp.Value;

                string inputData = File.ReadAllText($"wwwroot/lib/InitialSeedData/From{enName}To.txt");

                var splittedData = inputData.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < splittedData.Length; i += 2)
                {
                    var toCity = splittedData[i];
                    double km = double.Parse(splittedData[i + 1]);

                    string fromCity = bgName;//Replace this with new one city - Русе, Варна...

                    var currDistance = new Distance()
                    {
                        FromCity = fromCity,
                        ToCity = toCity,
                        DistanceInKM = km
                    };

                    await db.Distances.AddAsync(currDistance);
                }
            }

            db.SaveChanges();
        }
    }
}
