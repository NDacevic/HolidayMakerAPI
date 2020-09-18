using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI
{
    public class Addon
    {
        public int AddonId { get; set; }
        [StringLength(50)]
        public string AddonType { get; set; }
        public decimal Price { get; set; }
        
        public List<ReservationAddon> ReservationAddons { get; set; }
    }
}
