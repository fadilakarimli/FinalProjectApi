using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.SliderInfo;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly ISliderInfoRepository _sliderInfoRepo;
        private readonly IMapper _mapper;

        public SliderInfoService(ISliderInfoRepository sliderInfoRepo, IMapper mapper)
        {
            _sliderInfoRepo = sliderInfoRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(SliderInfoCreateDto dto)
        {
            var entity = _mapper.Map<SliderInfo>(dto);
            await _sliderInfoRepo.CreateAsync(entity);
        }

        public async Task<IEnumerable<SliderInfoDto>> GetAllAsync()
        {
            var entities = await _sliderInfoRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<SliderInfoDto>>(entities);
        }

        public async Task<SliderInfoDto> GetByIdAsync(int id)
        {
            var entity = await _sliderInfoRepo.GetByIdAsync(id);
            return _mapper.Map<SliderInfoDto>(entity);
        }

        public async Task EditAsync(int id, SliderInfoEditDto dto)
        {
            var entity = await _sliderInfoRepo.GetByIdAsync(id);
            if (entity == null) throw new Exception("SliderInfo tapılmadı");

            _mapper.Map(dto, entity);
            await _sliderInfoRepo.EditAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _sliderInfoRepo.GetWithExpressionAsync(x => x.Id == id);
            if (entity == null) throw new Exception("SliderInfo tapılmadı");
            await _sliderInfoRepo.DeleteAsync(entity);
        }

    }
}
