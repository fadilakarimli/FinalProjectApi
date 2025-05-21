using AutoMapper;
using Domain.Entities;
using Service.DTOs.Brand;
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
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<SliderEditDto, Slider>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //slider info
            CreateMap<SliderInfoCreateDto, SliderInfo>();
            CreateMap<SliderInfo, SliderInfoDto>(); 
            CreateMap<SliderInfoEditDto, SliderInfo>();
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


        }
    }
}
