using LogitWebApp.Data.Models;
using LogitWebApp.Services.Users;
using LogitWebApp.ViewModels.Votes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LogitWebApp.Controllers
{
    //[ApiController]
    //[Route("api/[Controller]")]
    public class VotesController /*: ControllerBase*/ : Controller
    {
        private readonly IVoteService voteService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public VotesController(IVoteService voteService, UserManager<ApplicationUser> userManager)
        {
            this.voteService = voteService;
            this.userManager = userManager;
        }

        //[Authorize]
        //[HttpPost]
        //[IgnoreAntiforgeryToken]
        //public async Task<ActionResult<VoteViewModel>> Vote(PostVoteInputModel input)
        //{
        //    //var userId = this.userManager.GetUserId(User);
        //    var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    await this.voteService.SetVoteAsync(input.DriverId, userId, input.Value);

        //    var averageVote = this.voteService.GetAverageVote(input.DriverId);

        //    return new VoteViewModel() { AverageVote = averageVote };
        //}

        [HttpGet]
        public async Task<IActionResult> Vote(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.voteService.SetVoteAsync(input.DriverId, userId, input.Value);

            // var averageVote = this.voteService.GetAverageVote(input.DriverId);

            return this.RedirectToAction("AllUserOrders", "Users");
        }




    }
}
