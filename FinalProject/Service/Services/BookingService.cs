using AutoMapper;
using Domain.Entities;
using Domain.Enums;
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

        public async Task<bool> UpdateStatusAsync(int bookingId, BookingStatus newStatus)
        {
            var booking = await _bookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
                return false;

            booking.Status = newStatus;
            await _bookingRepo.UpdateAsync(booking);

            var smtpHost = "smtp.gmail.com";
            var smtpPort = 587;
            var smtpUser = "fadilafk@code.edu.az";
            var smtpPass = "mleq kwrg luex jute"; 

            if (!string.IsNullOrWhiteSpace(booking.UserEmail))
            {
                try
                {
                    if (booking.Tour == null)
                        booking.Tour = await _tourRepo.GetByIdAsync(booking.TourId);

                    var mail = new System.Net.Mail.MailMessage();
                    mail.From = new System.Net.Mail.MailAddress(smtpUser);
                    mail.To.Add(booking.UserEmail);
                    mail.IsBodyHtml = false;

                    if (newStatus == BookingStatus.Accepted)
                    {
                        mail.Subject = "Booking Confirmed ✅ - Travil.az";
                        mail.Body = $@"
                            Dear Customer,

                            Great news! Your booking has been confirmed.

                            ═══════════════════════════════
                            BOOKING CONFIRMATION
                            ═══════════════════════════════
                            Tour: {booking.Tour?.Name}
                            Date: {booking.BookingDate:dd.MM.yyyy}
                            Adults: {booking.AdultsCount}
                            Children: {booking.ChildrenCount}
                            Total Amount: ${booking.TotalPrice} USD
                            ═══════════════════════════════

                            We're excited to have you join us on this amazing journey!

                            Please keep this confirmation for your records.

                            Best regards,
                            Travil.az Team
                            Your Travel Partner

                            ---
                            Thank you for choosing Travil.az
                            ";
                    }
                    else if (newStatus == BookingStatus.Cancelled)
                    {
                        mail.Subject = "Booking Cancellation - Travil.az ❌";
                        mail.Body = $@"
                            Dear Customer,

                            We regret to inform you that your booking has been cancelled.

                            ═══════════════════════════════
                            BOOKING DETAILS
                            ═══════════════════════════════
                            Tour: {booking.Tour?.Name}
                            Date: {booking.BookingDate:dd.MM.yyyy}
                            ═══════════════════════════════

                            If you have any questions or concerns regarding this cancellation, 
                            please don't hesitate to contact our customer service team.

                            We apologize for any inconvenience this may cause.

                            Best regards,
                            Travil.az Team
                            Your Travel Partner

                            ---
                            Thank you for choosing Travil.az
                            ";
                    }
                    using var smtp = new System.Net.Mail.SmtpClient(smtpHost, smtpPort)
                    {
                        Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass),
                        EnableSsl = true
                    };

                    await smtp.SendMailAsync(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Email göndərilərkən xəta: " + ex.Message);
                }
            }

            return true;
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
                TotalPrice = total,
                UserEmail = dto.UserEmail 
            };

            await _bookingRepo.CreateAsync(booking);

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            var bookings = await _bookingRepo.GetAllAsync();

            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);

            return bookingDtos;
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) throw new NotFoundException("Booking not found");

            if (booking.Tour == null)
                booking.Tour = await _tourRepo.GetByIdAsync(booking.TourId);

            return _mapper.Map<BookingDto>(booking);
        }


    }

}
