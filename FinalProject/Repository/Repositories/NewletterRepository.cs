using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class NewletterRepository : BaseRepository<NewLetter>, INewsletterRepository
    {
        public NewletterRepository(AppDbContext context) : base(context) { }

        public async Task AddEmailAsync(string email)
        {
            var newsletter = new NewLetter { Email = email };
            _context.NewLetters.Add(newsletter);
            await _context.SaveChangesAsync();
        }
    }
}
