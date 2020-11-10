using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext db;

        public HomeService(ApplicationDbContext db)
        {
            this.db = db;
        }
    }
}
