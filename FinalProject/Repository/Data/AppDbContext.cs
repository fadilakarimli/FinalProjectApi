using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SlidersInfo { get; set; }
        public DbSet<TrandingDestination> TrandingDestinations { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

    }
}
