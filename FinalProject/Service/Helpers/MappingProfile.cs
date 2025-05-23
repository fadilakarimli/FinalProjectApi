using AutoMapper;
using Domain.Entities;
using Service.DTOs.Blog;
using Service.DTOs.Brand;
using Service.DTOs.City;
using Service.DTOs.Country;
using Service.DTOs.DestinationFeature;
using Service.DTOs.NewLetter;
using Service.DTOs.Slider;
using Service.DTOs.SliderInfo;
using Service.DTOs.TeamMember;
using Service.DTOs.TrandingDestination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Slider
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<SliderEditDto, Slider>();
            //slider info
            CreateMap<SliderInfo, SliderInfoDto>().ReverseMap();
            CreateMap<SliderInfoCreateDto, SliderInfo>();
            CreateMap<SliderInfoEditDto, SliderInfo>().ReverseMap();
            //trandingdestinaions
            CreateMap<TrandingDestination, TrandingDestinationDto>().ReverseMap();
            CreateMap<TrandingDestinationCreateDto, TrandingDestination>();
            CreateMap<TrandingDestinationEditDto, TrandingDestination>();
            //brand
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<BrandEditDto, Brand>();
            //team
            CreateMap<TeamMemberCreateDto, TeamMember>();
            CreateMap<TeamMember, TeamMemberDto>().ReverseMap();
            CreateMap<TeamMemberEditDto, TeamMember>();
            //destinationfeature
            CreateMap<DestinationFeatureCreateDto, DestinationFeature>();
            CreateMap<DestinationFeature, DestinationFeatureDto>().ReverseMap();
            CreateMap<DestinationFeatureEditDto, DestinationFeature>();
            //blog
            CreateMap<BlogCreateDto, Blog>();
            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<BlogEditDto, Blog>();
            //newletter
            CreateMap<NewLetter, NewLetterDto>();

            // Country
            CreateMap<Country, CountryDto>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<CountryEditDto, Country>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CityCreateDto, City>();
            CreateMap<CityEditDto, City>();
            CreateMap<City, CityDto>()
                     .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));

        }
    }
}
