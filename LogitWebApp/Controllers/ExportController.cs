using LogitWebApp.Data;
using LogitWebApp.Services.Export;
using LogitWebApp.Services.Users;
using LogitWebApp.ViewModels.Drivers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using WHMS.Services.Common;

namespace LogitWebApp.Controllers
{
    public class ExportController : Controller
    {
        private readonly IViewRenderService viewRenderService;
        private readonly IHtmlToPdfConverter htmlToPdfConverter;
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUsersService usersService;

        public ExportController(
            IViewRenderService viewRenderService,
            IHtmlToPdfConverter htmlToPdfConverter,
            IWebHostEnvironment environment,
            ApplicationDbContext applicationDbContext, IUsersService usersService)
        {
            this.viewRenderService = viewRenderService;
            this.htmlToPdfConverter = htmlToPdfConverter;
            this.environment = environment;
            this.applicationDbContext = applicationDbContext;
            this.usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> GetPdf(string orderId)
        {
            var model = this.usersService.GetOrder(orderId);
            var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Users/GetOrderInPdf.cshtml", model);
            var fileContents = this.htmlToPdfConverter.Convert(this.environment.ContentRootPath, htmlData);
            return this.File(fileContents, "application/pdf");
        }

        [HttpGet]
        public async Task<IActionResult> GetPdf1(string id)
        {
            var model = this.applicationDbContext.Shipments.FirstOrDefault(s => s.Id == id);
            var htmlData = await this.viewRenderService.RenderToStringAsync("~/Views/Offers/Calculate.cshtml", model);
            var fileContents = this.htmlToPdfConverter.Convert(this.environment.ContentRootPath, htmlData);
            return this.File(fileContents, "application/pdf");

        }

    }
}
