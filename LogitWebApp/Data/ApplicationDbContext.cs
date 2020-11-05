using System;
using System.Collections.Generic;
using System.Text;
using LogitWebApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LogitWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<Distance> Distances { get; set; }


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

        }
    }
}
