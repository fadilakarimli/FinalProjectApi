using AutoMapper;
using Domain.Entities;
using Repository.Exceptions;
using Repository.Repositories.Interfaces;
using Service.DTOs.Booking;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ITourRepository _tourRepo;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepo, ITourRepository tourRepo, IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            _tourRepo = tourRepo;
            _mapper = mapper;
        }

        public async Task<BookingDto> CreateAsync(BookingCreateDto dto)
        {
            var tour = await _tourRepo.GetByIdAsync(dto.TourId);
            if (tour == null) throw new NotFoundException("Tour not found");

            var total = (dto.AdultsCount * tour.Price) + (dto.ChildrenCount * (tour.Price - 50));

            var booking = new Booking
            {
                TourId = dto.TourId,
                AdultsCount = dto.AdultsCount,
                ChildrenCount = dto.ChildrenCount,
                BookingDate = DateTime.UtcNow,
                TotalPrice = total
            };

            await _bookingRepo.CreateAsync(booking);

            return _mapper.Map<BookingDto>(booking);
        }
    }

}
