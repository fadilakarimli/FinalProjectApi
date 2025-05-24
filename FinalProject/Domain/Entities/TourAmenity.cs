using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourAmenity : BaseEntity
    {
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
