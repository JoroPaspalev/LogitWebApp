using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogitWebApp.Services.Users;
using LogitWebApp.ViewModels.Votes;

namespace LogitWebApp.Controllers
{
    //[ApiController]
    //[Route("api/[Controller]")]
    public class VotesController /*: ControllerBase*/ : Controller
    {
        private readonly IVoteService voteService;

        public VotesController(IVoteService voteService)
        {
            this.voteService = voteService;
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
