using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Offers
{
    public class ShortStringService : IShortStringService
    {
        public string ShortString(string text, int length)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= length)
            {
                return null;
            }

            return text.Substring(0, length)+"...";
        }
    }
}
