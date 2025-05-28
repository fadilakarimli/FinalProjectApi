using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tour : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; } 
        public string Duration { get; set; } 
        public int CountryCount { get; set; } 
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<TourActivity> TourActivities { get; set; }
        public ICollection<TourAmenity> TourAmenities { get; set; } 
    }
}
