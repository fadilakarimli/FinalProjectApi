using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Interfaces;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ISliderInfoService, SliderInfoService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITrandingDestinationService, TrandingDestinationService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<IDestinationFeatureService, DestinationFeatureService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<INewsLetterService, NewsLetterService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ITourService, TourService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<ICloudinaryManager, CloudinaryManager>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IInstagramService, InstagramService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
            services.AddScoped<ISpecialOfferModelService, SpecialOfferModelService>();
            services.AddScoped<IExperienceService, ExperienceService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IAboutAgencyService, AboutAgencyService>();
            return services;
        }
    }
}
