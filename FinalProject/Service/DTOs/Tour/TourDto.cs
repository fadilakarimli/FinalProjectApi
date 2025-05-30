using Service.DTOs.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Tour
{
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public int CountryCount { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string ImageUrl { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<string> ActivityNames { get; set; } 
        public List<string> Amenities { get; set; }
        public List<string> ExperienceNames { get; set; }
        public List<PlanDto> Plans { get; set; }
    }
}
