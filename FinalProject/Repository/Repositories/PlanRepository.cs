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
    public class PlanRepository : BaseRepository<Plan>, IPlanRepository
    {
        public PlanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateRangeAsync(IEnumerable<Plan> plans)
        {
            if (plans != null && plans.Any())
            {
                await _context.Set<Plan>().AddRangeAsync(plans);
                await _context.SaveChangesAsync();
            }
        }

    }
}
