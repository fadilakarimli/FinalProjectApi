using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SlidersInfo { get; set; }
        public DbSet<TrandingDestination> TrandingDestinations { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<DestinationFeature> DestinationFeatures { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<NewLetter> NewLetters { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<TourActivity> TourActivities { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<TourAmenity> TourAmenities { get; set; }
        public DbSet<Instagram> Instagrams { get; set; }
        public DbSet<SpecialOfferModel> SpecialOfferModels { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Plan> Plans { get; set; }
    }
}
