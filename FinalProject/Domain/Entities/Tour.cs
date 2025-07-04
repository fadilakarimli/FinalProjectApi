﻿using Domain.Common;
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
        public string Desc { get; set; }
        public int CountryCount { get; set; } 
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int Capacity { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<TourCity> TourCities { get; set; } = new List<TourCity>();
        public ICollection<TourActivity> TourActivities { get; set; }
        public ICollection<TourAmenity> TourAmenities { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
