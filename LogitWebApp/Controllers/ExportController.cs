﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using WHMS.Services.Common;
using LogitWebApp.Data;
using LogitWebApp.Services.Export;
using LogitWebApp.Services.Users;

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

        [Authorize]
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
