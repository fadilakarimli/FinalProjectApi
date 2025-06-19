using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories.Interfaces;
using Service.Helpers;
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
            services.AddScoped<ISendEmailService, SendEmailService>();
            services.AddScoped<ITourBannerService, TourBannerService>();
            services.AddScoped<ITourDetailBannerService, TourDetailBannerService>();
            services.AddScoped<IDestinationBannerService, DestinationBannerService>();
            services.AddScoped<IAboutBannerService, AboutBannerService>();
            services.AddScoped<IChooseUsAboutService, ChooseUsAboutService>();
            services.AddScoped<IAboutDestinationService, AboutDestinationService>();
            services.AddScoped<IAboutTravilService, AboutTravilService>();
            services.AddScoped<IAboutTeamMemberService, AboutTeamMemberService>();
            services.AddScoped<IAboutAppService, AboutAppService>();
            services.AddScoped<IAboutBlogService, AboutBlogService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IBookingService, BookingService>();

            //
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddScoped<UrlHelperService>();
            return services;
        }
    }   
}
