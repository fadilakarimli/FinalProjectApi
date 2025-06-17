    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Repository.Data;
    using Repository.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Linq.Expressions;
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
        .Include(t => t.TourCities)
            .ThenInclude(tc => tc.City)
        .Include(t => t.TourActivities)
            .ThenInclude(ta => ta.Activity)
        .Include(t => t.TourAmenities)
            .ThenInclude(ta => ta.Amenity)
        .Include(t => t.Experiences)
        .Include(t => t.Plans)
        .ToListAsync();

        }

        public async Task<Tour> GetByIdWithIncludesAsync(int id)
        {

            return await _context.Tours
                .Include(t => t.TourCities)
                    .ThenInclude(tc => tc.City)
                .Include(t => t.TourActivities)
                    .ThenInclude(ta => ta.Activity)
                .Include(t => t.TourAmenities)
                    .ThenInclude(ta => ta.Amenity)
                .Include(t => t.Experiences)
                .Include(t => t.Plans)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Tours.CountAsync();
        }

        public async Task<IEnumerable<Tour>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Tours.Skip((page * take) - take).Take(take).ToListAsync();
        }



        public async Task<IEnumerable<Tour>> SortAsync(string sortOrder)
        {
            var educations = await _context.Tours.ToListAsync();

            if (sortOrder == "asc")
            {
                educations = educations.OrderBy(e => e.Name).ToList();
            }
            else if (sortOrder == "desc")
            {
                educations = educations.OrderByDescending(e => e.Name).ToList();
            }

            return educations;
        }


        public async Task<IEnumerable<Tour>> SearchAsync(string city, string activity, DateTime? date, int? guestCount)
        {
            var query = _context.Tours
                .Include(t => t.TourCities).ThenInclude(tc => tc.City)
                .Include(t => t.TourActivities).ThenInclude(ta => ta.Activity)
                .AsQueryable();

            // 🔄 City ID-lərə görə filtrlə
            if (!string.IsNullOrWhiteSpace(city))
            {
                var cityIds = city.Split(',').Select(id => int.TryParse(id, out var val) ? val : -1).ToList();
                query = query.Where(t => t.TourCities.Any(tc => cityIds.Contains(tc.City.Id)));
            }

            // 🔄 Activity ID-lərə görə filtrlə
            if (!string.IsNullOrWhiteSpace(activity))
            {
                var activityIds = activity.Split(',').Select(id => int.TryParse(id, out var val) ? val : -1).ToList();
                query = query.Where(t => t.TourActivities.Any(ta => activityIds.Contains(ta.Activity.Id)));
            }

            if (date.HasValue)
            {
                query = query.Where(t => t.CreatedDate.Date == date.Value.Date);
            }

            if (guestCount.HasValue)
            {
                query = query.Where(t => t.Capacity >= guestCount.Value);
            }

            return await query.ToListAsync();
        }

        public IQueryable<Tour> GetAllWithIncludesQueryable(params Expression<Func<Tour, object>>[] includes)
        {
            IQueryable<Tour> query = _context.Tours.AsQueryable();

            if (includes != null && includes.Any())
            {
                foreach (var includeExpression in includes)
                {
                    query = query.Include(includeExpression);
                }
            }

            return query;
        }

        public async Task<IEnumerable<Tour>> GetAllForFilterAsync()
        {
            return await _context.Tours
                .Include(t => t.TourCities).ThenInclude(tc => tc.City)
                .Include(t => t.TourActivities).ThenInclude(ta => ta.Activity)
                .Include(t => t.TourAmenities).ThenInclude(ta => ta.Amenity)
                .Include(t => t.Experiences)
                .Include(t => t.Plans)
                .ToListAsync();
        }

    }
}
