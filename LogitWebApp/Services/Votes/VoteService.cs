using System.Linq;
using System.Threading.Tasks;
using LogitWebApp.Data;
using LogitWebApp.Data.Models;

namespace LogitWebApp.Services.Users
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext db;

        public VoteService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public double GetAverageVote(string driverId)
        {
            double averageVote = this.db.Votes.Where(v => v.DriverId == driverId).Average(x => x.Value);
            return averageVote;
        }

        public async Task SetVoteAsync(string driverId, string userId, byte voteValue)
        {
            Vote currVote = this.db.Votes.FirstOrDefault(v => v.UserId == userId && v.DriverId == driverId);

            if (currVote == null)
            {
                currVote = new Vote
                {
                    UserId = userId,
                    DriverId = driverId,
                    Value = voteValue
                };
                await this.db.Votes.AddAsync(currVote);
            }
            currVote.Value = voteValue;            
            await this.db.SaveChangesAsync();
        }
    }
}
