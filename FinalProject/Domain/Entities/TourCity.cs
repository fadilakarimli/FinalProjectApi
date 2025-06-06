using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourCity : BaseEntity
    {
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
