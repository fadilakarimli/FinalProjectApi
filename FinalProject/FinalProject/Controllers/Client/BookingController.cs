using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Booking;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.Client
{
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
        {
            var result = await _bookingService.CreateAsync(dto);
            return Ok(result);
        }
    }
}
