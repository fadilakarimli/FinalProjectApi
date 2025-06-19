using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
