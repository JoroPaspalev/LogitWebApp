using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Offers
{
    public interface IShortStringService
    {
        string ShortString(string text, int length);
    }
}
