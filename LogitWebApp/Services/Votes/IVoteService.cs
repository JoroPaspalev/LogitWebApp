using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Users
{
    public interface IVoteService
    {
        Task SetVoteAsync(string driverId, string userId, byte voteValue);

        double GetAverageVote(string driverId);
    }
}
