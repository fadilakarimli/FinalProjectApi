using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                          .Include(b => b.Tour)  // Tour məlumatlarını yüklə
                          .ToListAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Entry(booking).Property(x => x.Status).IsModified = true;
            await _context.SaveChangesAsync();
        }



    }
}
