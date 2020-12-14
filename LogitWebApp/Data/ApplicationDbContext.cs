using System;
using System.Collections.Generic;
using System.Text;

using LogitWebApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LogitWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<Distance> Distances { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.; Database=LogitWebApp;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ////Това добавя нова Role с името Admin в таблицата AspNetRoles
            //builder.Entity<IdentityRole>()
            //    .HasData(new IdentityRole
            //    {
            //        Name = "Admin",
            //        NormalizedName = "Admin".ToUpper()
            //    });


            builder.Entity<Shipment>()
                 .HasOne(x => x.LoadingAddress)
                 .WithMany(x => x.ShipmentsLoading)
                 .HasForeignKey(x => x.LoadingAddressId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Shipment>()
                .HasOne(x => x.UnloadingAddress)
                .WithMany(x => x.ShipmentsUnloading)
                .HasForeignKey(x => x.UnloadingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Shipment>()
              .HasOne(x => x.Sender)
              .WithMany(x => x.ShipmentSenders)
              .HasForeignKey(x => x.SenderId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Shipment>()
              .HasOne(x => x.Receiver)
              .WithMany(x => x.ShipmentReceivers)
              .HasForeignKey(x => x.ReceiverId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Shipment>()
                 .HasOne(x => x.FromCity)
                 .WithMany(x => x.ShipmentsFromThisCity)
                 .HasForeignKey(x => x.FromCityId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Shipment>()
                .HasOne(x => x.ToCity)
                .WithMany(x => x.ShipmentsToThisCity)
                .HasForeignKey(x => x.ToCityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.DriverVotes)
                .WithOne(x => x.Driver)
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
