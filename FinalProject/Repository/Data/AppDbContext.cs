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
        public DbSet<AboutAgency> AboutAgencies { get; set; }
        public DbSet<TourBanner> TourBanners { get; set; }
        public DbSet<TourDetailBanner> TourDetailBanners { get; set; }
        public DbSet<DestinationBanner> DestinationBanners { get; set; }
        public DbSet<AboutBanner> AboutBanners { get; set; }
        public DbSet<ChooseUsAbout> ChooseUsAbouts { get; set; }
        public DbSet<AboutDestination> AboutDestinations { get; set; }
        public DbSet<AboutTravil> AboutTravils { get; set; }
        public DbSet<AboutTeamMember> AboutTeamMembers { get; set; }
        public DbSet<AboutApp> AboutApps { get; set; }
        public DbSet<AboutBlog> AboutBlogs { get; set; }
        public DbSet<Setting> Settings { get; set; }



    }
}
