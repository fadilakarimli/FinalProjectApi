using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.DestinationFeature
{
    public class DestinationFeatureEditDto
    {
        public string Title { get; set; }
        public int TourCount { get; set; }
        public decimal PriceFrom { get; set; }
        public IFormFile? IconImage { get; set; }
    }
}
