using LogitWebApp.Data;
using LogitWebApp.Data.Models;
using LogitWebApp.ViewModels.Drivers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using static LogitWebApp.Common.GlobalConstants;
using System.Linq;
using System.Threading.Tasks;

namespace LogitWebApp.Services.Drivers
{
    public class DriversService : IDriversService
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public DriversService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task AddDriver(DriverInputModel input)
        {
            var currDriver = new ApplicationUser
            {
                Email = input.Email,
                UserName = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber
            };

            var result = await this.userManager.CreateAsync(currDriver, input.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(currDriver, Driver_RoleName);
            }
        }

        public IEnumerable<AllShipmentsWithAddresses> GetAllShipments()
        {
            return this.db.Shipments
                .Where(s => s.IsDelivered == false && s.LoadingAddress != null && s.UnloadingAddress != null && s.DriverId == null)
                .Select(x => new AllShipmentsWithAddresses
                {
                    Id = x.Id,
                    Length = x.Length,
                    Width = x.Width,
                    Height = x.Height,
                    Weight = x.Weight,
                    CountOfPallets = x.CountOfPallets,
                    LoadingAddress = x.LoadingAddress.ToString(),
                    UnloadingAddress = x.UnloadingAddress.ToString(),
                    IsFragile = x.IsFragile == true ? "Да" : "Не",
                    LoadingDate = x.LoadingDate != null ? (DateTime)x.LoadingDate : DateTime.Now,
                    //UnloadingDate = x.UnloadingDate.ToString().Substring(0, 10)      
                    UnloadingDate = x.UnloadingDate ?? DateTime.Now
                })
                .ToList();
        }

        public IEnumerable<AllShipmentsWithAddresses> GetMyShipments(string driverId)
        {
            var myShipments = this.db.Shipments
                .Where(x => x.DriverId == driverId && x.LoadingAddress != null && x.UnloadingAddress != null)
                .Select(s => new AllShipmentsWithAddresses
                {
                    Id = s.Id,
                    LoadingAddress = s.Sender.ToString() + ", " + s.LoadingAddress.ToString(),
                    UnloadingAddress = s.Receiver.ToString() + ", " + s.UnloadingAddress.ToString(),
                    Description = s.Description,
                    Comment = s.Comment,
                    CountOfPallets = s.CountOfPallets,
                    Length = s.Length,
                    Width = s.Width,
                    Weight = s.Weight,
                    Height = s.Height,
                    LoadingDate = s.LoadingDate ?? DateTime.UtcNow,
                    UnloadingDate = s.UnloadingDate ?? DateTime.UtcNow,
                    IsDelivered = s.IsDelivered
                }).ToList();

            return myShipments;
        }

        public void ChangeShipmentData(EditShipment input)
        {
            var currShipment = this.db.Shipments.FirstOrDefault(s => s.Id == input.ShipmentId);

            currShipment.Width = input.Width;
            currShipment.Length = input.Length;
            currShipment.Height = input.Height;
            currShipment.Weight = input.Weight;
            currShipment.CountOfPallets = input.CountOfPallets ?? 0;
            currShipment.Comment = input.Comment;
            currShipment.IsDelivered = input.IsDelivered;

            this.db.SaveChanges();
        }

        public bool IsDriverExist(string email)
        {
            return this.userManager.Users.Any(u => u.Email == email) ? true : false;
        }

        public AllShipmentsWithAddresses GetShipment(string shipmentId)
        {
            var shipmentForEdititng = this.db.Shipments.Where(s => s.Id == shipmentId)
                .Select(s => new AllShipmentsWithAddresses
                {
                    Id = s.Id,
                    LoadingAddress = s.Sender.ToString() + ", " + s.LoadingAddress.ToString(),
                    UnloadingAddress = s.Receiver.ToString() + ", " + s.UnloadingAddress.ToString(),
                    Description = s.Description,
                    Comment = s.Comment,
                    CountOfPallets = s.CountOfPallets,
                    Length = s.Length,
                    Width = s.Width,
                    Weight = s.Weight,
                    Height = s.Height,
                    LoadingDate = s.LoadingDate ?? DateTime.UtcNow,
                    UnloadingDate = s.UnloadingDate ?? DateTime.UtcNow,
                    IsDelivered = s.IsDelivered
                })
                .First();

            return shipmentForEdititng;
        }

        public bool IsPhoneExist(string phone)
        {
            return this.userManager.Users.Any(u => u.PhoneNumber == phone) ? true : false;
        }

        public void AttachShipmentToDriver(string shipmentId, string userId)
        {
            var currShipment = this.db.Shipments.FirstOrDefault(s => s.Id == shipmentId);
            currShipment.DriverId = userId;
            this.db.SaveChanges();
        }

        public async Task<bool> DeleteDriver(string email)
        {
            var currUser = this.userManager.Users.FirstOrDefault(u => u.Email == email);

            var result = await this.userManager.DeleteAsync(currUser);

            return result.Succeeded ? true : false;
        }
    }
}
