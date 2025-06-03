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
    public class TrandingDestinationRepository : BaseRepository<TrandingDestination>, ITrandingDestinationRepository
    {
        public TrandingDestinationRepository(AppDbContext context) : base(context) { }

        public async Task<int> GetCountAsync()
        {
            return await _context.TrandingDestinations.CountAsync();
        }

        public async Task<IEnumerable<TrandingDestination>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.TrandingDestinations.Skip((page * take) - take).Take(take).ToListAsync();
        }


        public async Task<IEnumerable<TrandingDestination>> SortAsync(string sortOrder)
        {
            var educations = await _context.TrandingDestinations.ToListAsync();

            if (sortOrder == "asc")
            {
                educations = educations.OrderBy(e => e.Title).ToList();
            }
            else if (sortOrder == "desc")
            {
                educations = educations.OrderByDescending(e => e.Title).ToList();
            }

            return educations;
        }

    }
}
