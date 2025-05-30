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
        public class TourRepository : BaseRepository<Tour>, ITourRepository
        {
            public TourRepository(AppDbContext context) : base(context) { }

            public async Task<IEnumerable<Tour>> GetAllTourWithActivityAsync()
            {
            return await _context.Tours
                  .Include(t => t.City)
                  .Include(t => t.TourActivities).ThenInclude(ta => ta.Activity)
                  .Include(t => t.TourAmenities).ThenInclude(ta => ta.Amenity)
                  .Include(t => t.Experiences)
                  .Include(t => t.Plans) // Plans-ları da əlavə et
                  .ToListAsync();

            }
 
        public async Task<Tour> GetByIdWithIncludesAsync(int id)
        {
            return await _context.Tours
        .Include(t => t.City)
        .Include(t => t.TourActivities).ThenInclude(ta => ta.Activity)
        .Include(t => t.TourAmenities).ThenInclude(ta => ta.Amenity)
        .Include(t => t.Experiences)
        .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
