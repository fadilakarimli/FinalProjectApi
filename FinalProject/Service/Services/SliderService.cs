using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Slider;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;
        public SliderService(ISliderRepository sliderRepository, IMapper mapper)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(SliderCreateDto model)
        {
            var slider = _mapper.Map<Slider>(model);
            await _sliderRepository.CreateAsync(slider);
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(int id, SliderEditDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SliderDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SliderDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
