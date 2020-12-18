using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using LogitWebApp.Services.Users;
using Xunit;
using System.Threading.Tasks;

namespace LogitWebApp.Tests.Services
{
    public class VoteServiceTests
    {
        [Fact]
        public void GetAverageVoteShouldreturnAverageVote()
        {
            var user1 = new ApplicationUser { UserName = "User1" };
            var user2 = new ApplicationUser { UserName = "User2" };
            var user3 = new ApplicationUser { UserName = "User3" };
            var driver = new ApplicationUser { UserName = "Driver" };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(optionsBuilder.Options);

            db.Users.AddRange(user1, user2, user3, driver);
            db.SaveChanges();

            db.Votes.Add(new Vote { User = user1, Driver = driver, Value = 5 });
            db.Votes.Add(new Vote { User = user2, Driver = driver, Value = 1 });
            db.Votes.Add(new Vote { User = user3, Driver = driver, Value = 4 });
            db.SaveChanges();

            var voteService = new VoteService(db);
            var result = voteService.GetAverageVote(driver.Id);

            Assert.Equal(3.3333333333333335, result);
        }

        [Fact]
        public async Task SetVoteAsyncShouldSetVoteToTheDriver()
        {
            var user1 = new ApplicationUser { UserName = "User1" };
            var user2 = new ApplicationUser { UserName = "User2" };
            var user3 = new ApplicationUser { UserName = "User3" };
            var driver = new ApplicationUser { UserName = "Driver" };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(optionsBuilder.Options);

            db.Users.AddRange(user1, user2, user3, driver);
            db.SaveChanges();

            db.Votes.Add(new Vote { User = user1, Driver = driver, Value = 5 });
            db.Votes.Add(new Vote { User = user2, Driver = driver, Value = 1 });
            db.Votes.Add(new Vote { User = user3, Driver = driver, Value = 4 });
            db.SaveChanges();

            var voteService = new VoteService(db);
            await voteService.SetVoteAsync(driver.Id, user3.Id, 9);
            var result = voteService.GetAverageVote(driver.Id);

            Assert.Equal(5, result);
        }

        [Fact]
        public async Task SetVoteAsyncShouldSetNewVoteToTheDriverIfNotExistSuchVote()
        {
            var user1 = new ApplicationUser { UserName = "User1" };
            var user2 = new ApplicationUser { UserName = "User2" };
            var user3 = new ApplicationUser { UserName = "User3" };
            var user4 = new ApplicationUser { UserName = "User4" };
            var driver = new ApplicationUser { UserName = "Driver" };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(optionsBuilder.Options);

            db.Users.AddRange(user1, user2, user3, driver);
            db.SaveChanges();

            db.Votes.Add(new Vote { User = user1, Driver = driver, Value = 5 });
            db.Votes.Add(new Vote { User = user2, Driver = driver, Value = 1 });
            db.Votes.Add(new Vote { User = user3, Driver = driver, Value = 4 });
            db.SaveChanges();

            var voteService = new VoteService(db);
            await voteService.SetVoteAsync(driver.Id, user4.Id, 2);
            var result = voteService.GetAverageVote(driver.Id);

            Assert.Equal(3, result);
        }
    }
}
