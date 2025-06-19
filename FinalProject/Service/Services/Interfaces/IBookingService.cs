using Service.DTOs.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateAsync(BookingCreateDto dto);
    }
}
