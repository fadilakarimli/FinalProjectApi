using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Tour
{
    public class TourFilterDto
    {
        public List<string>? CityNames { get; set; }
        public List<string>? ActivityNames { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? GuestCount { get; set; }
        public List<string>? AmenityNames { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

    }
}
