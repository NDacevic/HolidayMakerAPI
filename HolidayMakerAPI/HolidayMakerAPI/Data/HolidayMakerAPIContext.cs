﻿using System;
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

        public DbSet<HolidayMakerAPI.User> User { get; set; }

        public DbSet<HolidayMakerAPI.Models.Reservation> Reservation { get; set; }
    }
}
