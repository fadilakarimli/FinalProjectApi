using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        public int Day { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
