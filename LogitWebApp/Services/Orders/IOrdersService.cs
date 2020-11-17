using LogitWebApp.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Orders
{
    public interface IOrdersService
    {       
        string CreateOrder(AddressInputModel input, string userId);
    }
}
