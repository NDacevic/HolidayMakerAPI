using HolidayMakerAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI
{
    public class ReservationAddon
    {
        public int ReservationId { get; set; }
        public int AddonId { get; set; }
        [NotMapped]
        public Reservation Reservation { get; set; }
        [NotMapped]
        public Addon Addon { get; set; }

    }
}
