using AutoMapper;
using Domain.Entities;
using Service.DTOs.Account;
using Service.DTOs.Activity;
using Service.DTOs.Amenity;
using Service.DTOs.Blog;
using Service.DTOs.Brand;
using Service.DTOs.City;
using Service.DTOs.Country;
using Service.DTOs.DestinationFeature;
using Service.DTOs.Instagram;
using Service.DTOs.NewLetter;
using Service.DTOs.Slider;
using Service.DTOs.SliderInfo;
using Service.DTOs.SpecialOffer;
using Service.DTOs.TeamMember;
using Service.DTOs.Tour;
using Service.DTOs.TrandingDestination;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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
            //tour
            CreateMap<TourCreateDto, Tour>()
                      .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<TourEditDto, Tour>()
                      .ForMember(dest => dest.Image, opt => opt.Ignore()); 
            CreateMap<Tour, TourEditDto>()
                      .ForMember(dest => dest.ExistingImageUrl, opt => opt.MapFrom(src => src.Image));
            CreateMap<Tour, TourDto>()
           .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
           .ForMember(dest => dest.ActivityNames, opt => opt.MapFrom(src =>
           src.TourActivities.Select(ta => ta.Activity.Name).ToList()))
          .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src =>
           src.TourAmenities.Select(ta => ta.Amenity.Name).ToList()))
          .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image));
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




        }
    }
}
