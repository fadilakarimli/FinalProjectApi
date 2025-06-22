using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Booking : BaseEntity
    {
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public DateTime BookingDate { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; }


        public string UserEmail { get; set; }

    }
}
