﻿using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Service.DTOs.AboutAgency;
using Service.DTOs.AboutApp;
using Service.DTOs.AboutBanner;
using Service.DTOs.AboutBlog;
using Service.DTOs.AboutDestination;
using Service.DTOs.AboutTeamMember;
using Service.DTOs.AboutTravil;
using Service.DTOs.Account;
using Service.DTOs.Activity;
using Service.DTOs.Amenity;
using Service.DTOs.Blog;
using Service.DTOs.Booking;
using Service.DTOs.Brand;
using Service.DTOs.ChooseUsAbout;
using Service.DTOs.City;
using Service.DTOs.Contact;
using Service.DTOs.Country;
using Service.DTOs.DestinationBanner;
using Service.DTOs.DestinationFeature;
using Service.DTOs.Experience;
using Service.DTOs.Instagram;
using Service.DTOs.NewLetter;
using Service.DTOs.Plan;
using Service.DTOs.Review;
using Service.DTOs.Setting;
using Service.DTOs.Slider;
using Service.DTOs.SliderInfo;
using Service.DTOs.SpecialOffer;
using Service.DTOs.TeamMember;
using Service.DTOs.Tour;
using Service.DTOs.TourBanner;
using Service.DTOs.TourDetailBanner;
using Service.DTOs.TrandingDestination;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //booking
            CreateMap<Booking, BookingDto>()
           .ForMember(dest => dest.TourName, opt => opt.MapFrom(src => src.Tour.Name))
               .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail));
            CreateMap<BookingCreateDto, Booking>();

            //review
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<Review, ReviewListDto>();
            //Slider
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<SliderEditDto, Slider>()
                     .ForMember(dest => dest.Img, opt => opt.Ignore());
            //slider info
            CreateMap<SliderInfo, SliderInfoDto>().ReverseMap();
            CreateMap<SliderInfoCreateDto, SliderInfo>();
            CreateMap<SliderInfoEditDto, SliderInfo>().ReverseMap();
            //trandingdestinaions
            CreateMap<TrandingDestination, TrandingDestinationDto>().ReverseMap();
            CreateMap<TrandingDestinationCreateDto, TrandingDestination>();
            CreateMap<TrandingDestinationEditDto, TrandingDestination>()
                     .ForMember(dest => dest.Image, opt => opt.Ignore());
            //brand
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<BrandEditDto, Brand>();
            //team
            CreateMap<TeamMemberCreateDto, TeamMember>();
            CreateMap<TeamMember, TeamMemberDto>().ReverseMap();
            CreateMap<TeamMemberEditDto, TeamMember>()
                  .ForMember(dest => dest.Image, opt => opt.Ignore());
            //destinationfeature
            CreateMap<DestinationFeatureCreateDto, DestinationFeature>();
            CreateMap<DestinationFeature, DestinationFeatureDto>().ReverseMap();
            CreateMap<DestinationFeatureEditDto, DestinationFeature>()
                 .ForMember(dest => dest.IconImage, opt => opt.Ignore());
            //blog
            CreateMap<BlogCreateDto, Blog>();
            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<BlogEditDto, Blog>()
           .ForMember(dest => dest.Image, opt => opt.Ignore());
            //tour filter

            //contact 
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactCreateDto, Contact>();

            //newletter
            CreateMap<NewLetter, NewLetterDto>();

            // Country
            CreateMap<Country, CountryDto>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<CountryEditDto, Country>()
                     .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            //city
            CreateMap<CityCreateDto, City>();
            CreateMap<CityEditDto, City>();
            CreateMap<City, CityDto>()
                     .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
        //    //tour
        //    CreateMap<TourCreateDto, Tour>()
        //              .ForMember(dest => dest.Image, opt => opt.Ignore());
        //    CreateMap<TourEditDto, Tour>()
        //              .ForMember(dest => dest.Image, opt => opt.Ignore());
        //    CreateMap<Tour, TourEditDto>();
        //    CreateMap<Tour, TourDto>()
        //.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
        //.ForMember(dest => dest.ActivityNames, opt => opt.MapFrom(src =>
        //    src.TourActivities.Select(ta => ta.Activity.Name).ToList()))
        //.ForMember(dest => dest.Amenities, opt => opt.MapFrom(src =>
        //    src.TourAmenities.Select(ta => ta.Amenity.Name).ToList()))
        //.ForMember(dest => dest.ExperienceNames, opt => opt.MapFrom(src =>
        //    src.Experiences.Select(e => e.Name).ToList()))
        //.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image))
        //.ForMember(dest => dest.Plans, opt => opt.MapFrom(src => src.Plans));  // Plans map edildi
            // Activity
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityCreateDto, Activity>();
            CreateMap<ActivityEditDto, Activity>().ReverseMap();
            //amenity
            CreateMap<Amenity, AmenityDto>();
            CreateMap<AmenityCreateDto, Amenity>();
            CreateMap<AmenityEditDto, Amenity>().ReverseMap();
            //instagram
            CreateMap<Instagram, InstagramDto>(); 
            CreateMap<InstagramEditDto, Instagram>() 
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<InstagramCreateDto, Instagram>();
            //register
            CreateMap<RegisterDto, AppUser>();

            CreateMap<AppUser, UserProfileDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); 

            CreateMap<UpdateProfileDto, AppUser>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            //specialoffer
            CreateMap<SpecialOfferModel, SpecialOfferDto>();

            CreateMap<SpecialOfferCreateDto, SpecialOfferModel>()
                .ForMember(dest => dest.BackgroundImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.DiscountImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.BagImageUrl, opt => opt.Ignore());

            CreateMap<SpecialOfferEditDto, SpecialOfferModel>()
                .ForMember(dest => dest.BackgroundImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.DiscountImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.BagImageUrl, opt => opt.Ignore());
            //experience
            CreateMap<Experience, ExperienceDto>()
            .ForMember(dest => dest.TourName, opt => opt.MapFrom(src => src.Tour.Name));
            CreateMap<ExperienceCreateDto, Experience>();
            CreateMap<ExperienceEditDto, Experience>().ReverseMap();
            //tour
            CreateMap<TourCreateDto, Tour>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<TourEditDto, Tour>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity));

            CreateMap<Tour, TourEditDto>();
            CreateMap<Tour, TourFilterDto>();

            CreateMap<Tour, TourDto>()
  .ForMember(dest => dest.CreatedDate, opt =>
      opt.MapFrom(src => src.CreatedDate.ToString("MM.dd.yyyy")))
  .ForMember(dest => dest.StartDate, opt =>
      opt.MapFrom(src => src.StartDate.ToString("MM.dd.yyyy")))
  .ForMember(dest => dest.EndDate, opt =>
      opt.MapFrom(src => src.EndDate.ToString("MM.dd.yyyy")))
  .ForMember(dest => dest.ActivityNames, opt => opt.MapFrom(src =>
      src.TourActivities
          .Where(ta => ta.Activity != null && ta.Activity.Name != null)
          .Select(ta => ta.Activity.Name)
          .ToList()))
  .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src =>
      src.TourAmenities
          .Where(ta => ta.Amenity != null && ta.Amenity.Name != null)
          .Select(ta => ta.Amenity.Name)
          .ToList()))
  .ForMember(dest => dest.ExperienceNames, opt => opt.MapFrom(src =>
      src.Experiences.Select(e => e.Name).ToList()))
  .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image))
  .ForMember(dest => dest.Plans, opt => opt.MapFrom(src => src.Plans))
  .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
  .ForMember(dest => dest.CityIds, opt => opt.MapFrom(src =>
      src.TourCities.Select(tc => tc.CityId).ToList()))
  .ForMember(dest => dest.CityNames, opt => opt.MapFrom(src =>
      src.TourCities.Where(tc => tc.City != null).Select(tc => tc.City.Name).ToList()))
  .ForMember(dest => dest.ActivityIds, opt => opt.MapFrom(src =>
      src.TourActivities.Select(ta => ta.ActivityId).ToList()))
  .ForMember(dest => dest.AmenityIds, opt => opt.MapFrom(src =>
      src.TourAmenities.Select(ta => ta.AmenityId).ToList()))
  .ForMember(dest => dest.CountryIds, opt => opt.MapFrom(src =>
      src.TourCities
         .Where(tc => tc.City != null)
         .Select(tc => tc.City.CountryId)
         .Distinct()    
         .ToList()))

  .ForMember(dest => dest.CountryNames, opt => opt.MapFrom(src =>
      src.TourCities
         .Where(tc => tc.City != null && tc.City.Country != null)
         .Select(tc => tc.City.Country.Name)
         .Distinct()
         .ToList()));



            // Plan 
            CreateMap<Plan, PlanDto>();
            CreateMap<PlanCreateDto, Plan>();
            CreateMap<PlanEditDto, Plan>();
            //about agency
            CreateMap<AboutAgencyCreateDto, AboutAgency>()
               .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<AboutAgencyEditDto, AboutAgency>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<AboutAgency, AboutAgencyDto>();
            //tourbanner
            CreateMap<TourBanner, TourBannerDto>();
            CreateMap<TourBannerCreateDto, TourBanner>();
            CreateMap<TourBannerEditDto, TourBanner>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            //tourdetailbanner
            CreateMap<TourDetailBannerCreateDto, TourDetailBanner>();
            CreateMap<TourDetailBannerEditDto, TourDetailBanner>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<TourDetailBanner, TourDetailBannerDto>();
            //destination 
            CreateMap<DestinationBannerCreateDto, DestinationBanner>();
            CreateMap<DestinationBannerEditDto, DestinationBanner>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<DestinationBanner, DestinationBannerDto>();
            //aboutbanner
            CreateMap<AboutBannerCreateDto, AboutBanner>();
            CreateMap<AboutBannerEditDto, AboutBanner>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<AboutBanner, AboutBannerDto>();
            //chooseusabout
            CreateMap<ChooseUsAboutCreateDto, ChooseUsAbout>();
            CreateMap<ChooseUsAboutEditDto, ChooseUsAbout>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<ChooseUsAbout, ChooseUsAboutDto>();
            //aboutdestination
            CreateMap<AboutDestinationCreateDto, AboutDestination>();
            CreateMap<AboutDestinationEditDto, AboutDestination>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<AboutDestination, AboutDestinationDto>();
            //abouttravil
            CreateMap<AboutTravilCreateDto, AboutTravil>();
            CreateMap<AboutTravilEditDto, AboutTravil>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.SmallImage, opt => opt.Ignore());
            CreateMap<AboutTravil, AboutTravilDto>();
            //aboutteammember
            CreateMap<AboutTeamMemberCreateDto, AboutTeamMember>();
            CreateMap<AboutTeamMemberEditDto, AboutTeamMember>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<AboutTeamMember, AboutTeamMemberDto>();
            //aboutapp
            CreateMap<AboutAppCreateDto, AboutApp>();
            CreateMap<AboutAppEditDto, AboutApp>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.AppleImage, opt => opt.Ignore())
                .ForMember(dest => dest.PlayStoreImage, opt => opt.Ignore())
                .ForMember(dest => dest.BackgroundImage, opt => opt.Ignore());
            CreateMap<AboutApp, AboutAppDto>();
            //aboutblog
            CreateMap<AboutBlogCreateDto, AboutBlog>();
            CreateMap<AboutBlogEditDto, AboutBlog>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<AboutBlog, AboutBlogDto>();

            //setting
            CreateMap<SettingCreateDto, Setting>();
            CreateMap<SettingEditDto, Setting>();
            CreateMap<Setting, SettingDto>();

            //account
            CreateMap<AppUser, UserDto>();
            CreateMap<IdentityRole, RoleDto>();



        }
    }
}
