using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Tour
{
    public class TourEditDto
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public int CountryCount { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int CityId { get; set; }

        public IFormFile? ImageFile { get; set; }

        // 🔥 Bunları əlavə et:
        public List<int> ActivityIds { get; set; }
        public List<int> AmenityIds { get; set; }
        public List<int> ExperienceIds { get; set; }
    }
}
