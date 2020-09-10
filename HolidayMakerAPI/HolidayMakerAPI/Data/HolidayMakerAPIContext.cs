using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI;
using HolidayMakerAPI.Models;

namespace HolidayMakerAPI.Data
{
    public class HolidayMakerAPIContext : DbContext
    {
        public HolidayMakerAPIContext (DbContextOptions<HolidayMakerAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationAddon>().HasKey(ra => new { ra.AddonId, ra.ReservationId });
        }

        public DbSet<HolidayMakerAPI.User> User { get; set; }

        public DbSet<HolidayMakerAPI.Models.Reservation> Reservation { get; set; }

        public DbSet<HolidayMakerAPI.ReservationAddon> ReservationAddon { get; set; }
    }
}
