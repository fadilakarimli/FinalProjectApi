using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ISliderInfoRepository, SliderInfoRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<ITrandingDestinationRepository, TrandingDestinationRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            services.AddScoped<IDestinationFeatureRepository, DestinationFeatureRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<INewsletterRepository, NewletterRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IInstagramRepository, InstagramRepository>();
            services.AddScoped<ISpecialOfferModelRepository, SpecialOfferModelRepository>();
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IAboutAgencyRepository, AboutAgencyRepository>();
            services.AddScoped<ITourBannerRepository, TourBannerRepository>();
            services.AddScoped<ITourDetailBannerRepository, TourDetailBannerRepository>();
            services.AddScoped<IDestinationBannerRepository, DestinationBannerRepository>();
            services.AddScoped<IAboutBannerRepository, AboutBannerRepository>();
            services.AddScoped<IChooseUsAboutRepository, ChooseUsAboutRepository>();
            services.AddScoped<IAboutDestinationRepository, AboutDestinationRepository>();
            return services;
        }
    }
}
