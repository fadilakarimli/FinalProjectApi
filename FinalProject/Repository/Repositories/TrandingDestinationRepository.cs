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
    public class TrandingDestinationRepository : BaseRepository<TrandingDestination>, ITrandingDestinationRepository
    {
        public TrandingDestinationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
