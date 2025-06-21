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
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Blog>> SearchAsync(string query)
        {
            return await _context.Blogs
                .Where(b => b.Title.Contains(query) || b.Content.Contains(query))
                .ToListAsync();
        }

    }
}
