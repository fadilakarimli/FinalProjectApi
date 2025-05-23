using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<TourActivity> TourActivities { get; set; }
    }
}
