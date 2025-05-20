using AutoMapper;
using Domain.Entities;
using Service.DTOs.Slider;
using Service.DTOs.SliderInfo;
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
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<Slider, SliderDto>().ReverseMap();

            CreateMap<SliderEditDto, Slider>()
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //slider info

            CreateMap<SliderInfoCreateDto, SliderInfo>();
            CreateMap<SliderInfo, SliderInfoDto>(); 
            CreateMap<SliderInfoEditDto, SliderInfo>();
        }
    }
}
