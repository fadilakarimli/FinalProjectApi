using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Booking
{
    public class BookingCreateDto
    {
        public int TourId { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
    }
}
