using AutoMapper;
using Domain.Entities;
using Service.DTOs.Brand;
using Service.DTOs.DestinationFeature;
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






        }
    }
}
