using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int NumberOfGuests { get; set; }
        [NotMapped]
        public List<Addon> Addons { get; set; }

        public int HomeId { get; set; }
        public Home Home { get; set; }
        public User User { get; set; }
        public List<ReservationAddon> ReservationAddons { get; set; }
    }
}
