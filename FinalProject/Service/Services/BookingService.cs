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

            // Status accepted olarsa, mail göndər
            if (newStatus == BookingStatus.Accepted && !string.IsNullOrWhiteSpace(booking.UserEmail))
            {
                try
                {
                    // Tour null ola bilər, yoxla və yüklə
                    if (booking.Tour == null)
                        booking.Tour = await _tourRepo.GetByIdAsync(booking.TourId);

                    var smtpHost = "smtp.gmail.com";
                    var smtpPort = 587;
                    var smtpUser = "fadilafk@code.edu.az";
                    var smtpPass = "fqen ovmf kvou rvos"; // App password

                    var mail = new System.Net.Mail.MailMessage();
                    mail.From = new System.Net.Mail.MailAddress(smtpUser);
                    mail.To.Add(booking.UserEmail);
                    mail.Subject = "Rezervasiyanız təsdiqləndi ✅";
                    mail.Body = $@"
                Hörmətli istifadəçi,

                Sizin rezervasiyanız təsdiqləndi.

                Tur: {booking.Tour?.Name}
                Tarix: {booking.BookingDate:dd.MM.yyyy}
                Yetişkin sayı: {booking.AdultsCount}
                Uşaq sayı: {booking.ChildrenCount}
                Ümumi məbləğ: {booking.TotalPrice} USD

                Təşəkkür edirik, Travil.az komandasından.
            ";
                    mail.IsBodyHtml = false;

                    using var smtp = new System.Net.Mail.SmtpClient(smtpHost, smtpPort)
                    {
                        Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass),
                        EnableSsl = true
                    };

                    await smtp.SendMailAsync(mail);
                }
                catch (Exception ex)
                {
                    // Mail göndərərkən xətanı logla
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
                UserEmail = dto.UserEmail // ✅ burda yazılır
            };

            await _bookingRepo.CreateAsync(booking);

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            var bookings = await _bookingRepo.GetAllAsync();

            // AutoMapper ilə entity-ləri DTO-lara çeviririk
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);

            return bookingDtos;
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) throw new NotFoundException("Booking not found");

            // Tour adı DTO-ya əlavə olunur (əgər Tour lazy-loaded deyilsə, include etməlisən)
            if (booking.Tour == null)
                booking.Tour = await _tourRepo.GetByIdAsync(booking.TourId);

            return _mapper.Map<BookingDto>(booking);
        }


    }

}
