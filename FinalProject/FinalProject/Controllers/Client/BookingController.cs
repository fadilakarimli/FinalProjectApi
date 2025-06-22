using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Exceptions;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, BookingStatusUpdateDto dto)
        {
            var updated = await _bookingService.UpdateStatusAsync(id, dto.Status);
            if (!updated)
                return NotFound();

            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var booking = await _bookingService.GetByIdAsync(id);
                return Ok(booking);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }





    }
}
