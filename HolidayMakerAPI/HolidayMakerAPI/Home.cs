using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI
{
    public class Home
    {
        public int HomeId { get; set; }
        [StringLength(50)]
        public string HomeType { get; set; }
        public int Rooms { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        public decimal Price { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasWifi { get; set; }
        [StringLength(1000)]
        public string Image { get; set; }
        public bool HasHalfPension { get; set; }
        public bool HasFullPension { get; set; }
        public bool HasAllInclusive { get; set; }
        public bool HasExtraBed { get; set; }
        public int CityDistance { get; set; }
        public int BeachDistance { get; set; }
        public int NumberOfBeds { get; set; }
        public bool HasPool { get; set; }
        public bool AllowSmoking { get; set; }
        [StringLength(3000)]
        public string Description { get; set; }
        public bool AllowPets { get; set; }
        public int NumberOfRatings { get; set; }
        public int SumOfRatings { get; set; }

    }
}
