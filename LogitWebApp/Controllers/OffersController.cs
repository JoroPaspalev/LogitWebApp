using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogitWebApp.Services.Offers;
using LogitWebApp.ViewModels.Offer;
using LogitWebApp.Data.Models;

namespace LogitWebApp.Controllers
{   
    public class OffersController : Controller
    {
        private readonly IOffersService offersService;

        public OffersController(IOffersService offersService)
        {
            this.offersService = offersService;
        }

        public IActionResult ShipmentInSameCity(string cityName)
        {
            ViewBag.CityName = cityName;

            return this.View();
        }

        public async Task<IActionResult> Calculate(string ShipmentId)
        {            
            Shipment currShipment = this.offersService.GetShipmentById(ShipmentId);

            ShipmentViewModel offerForShipment = await this.offersService.GetOffer(currShipment);

            return this.View(offerForShipment);
        }






































        //[HttpPost]
        //public IActionResult Calculate(OfferInputModel input)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.Json(ModelState);
        //    }

        //    if (input.From.ToLower() == input.To.ToLower())
        //    {               
        //        return this.Redirect($"/Offers/ShipmentInSameCity?cityName={input.From}");
        //    }

        //    Shipment offerForShipment = this.offersService.GetOffer(input);

        //    return this.View(offerForShipment);


        ////Delete this test
        ////Това отговаря на this.IsUserLogIn(); в нашия MVC Framework
        //bool isUserLogin = this.User.Identity.IsAuthenticated;

        ////Ако искаме да вземем Id-то на User-а правим така
        //var userId = this.User.FindFirst("UserId"); //Това бърка през някакви claims за да вземе UserId-то


        //this.ViewBag.veryLongString = "fhdjkshfkjdshfkdshfkdshfjdkshfjkdshfjkdhfjkdshfjkhdsjkfhdkhfjdkshfdsjkhf";
        //this.ViewBag.alert = "<script>alert('You are hacked!')</script>";
        //this.ViewBag.div = "When we want to see that string <div> Hello </div> just pass it trough ViewBag to View. Razor Engine automatically encode that string for us";
        //this.ViewBag.div2 = "If we want pure text without to be encoded(escaped) we use @Html.Raw(); which give us this result";
        //this.ViewBag.div3 = "<div> Hello </div>";
        //this.ViewBag.div4 = "where <div> and </div> are removed because Razor View Engine is did not encoded tags and they are passed to Browser in pure Html. Browser understand this text like tags and render them";
        //this.ViewBag.myHtmlString = new HtmlString("<html> This is how you escape an HTML tag in Razor </html>");

        ////Тези двете правят едно и също нещо само че по два различни начина
        //this.ViewData["Price"] = this.offersService.GetOffer(input).Price;// Това е глобално dictionary и може да се достъпва от всички _Views и _Layout

        //}
    }
}
