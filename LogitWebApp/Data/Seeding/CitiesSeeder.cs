using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LogitWebApp.Data.Models;

namespace LogitWebApp.Data.Seeding
{
    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db, IServiceProvider serviceProvider)
        {
            HashSet<string> cities = new HashSet<string>()
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

            foreach (var cityName in cities)
            {
                City currCity = db.Cities.FirstOrDefault(c => c.Name == cityName);

                if (currCity == null)
                {
                    await db.Cities.AddAsync(new City { Name = cityName });                   
                }
            }
        }
    }
}
