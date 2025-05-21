using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DestinationFeature : BaseEntity
    {
        public string Title { get; set; }
        public int TourCount { get; set; } 
        public decimal PriceFrom { get; set; } 
        public string IconImage { get; set; } 
    }
}
