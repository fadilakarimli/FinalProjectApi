using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return services;
        }
    }
}
