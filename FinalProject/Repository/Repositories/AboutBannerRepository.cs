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
    public class AboutBannerRepository : BaseRepository<AboutBanner>, IAboutBannerRepository
    {
        public AboutBannerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
