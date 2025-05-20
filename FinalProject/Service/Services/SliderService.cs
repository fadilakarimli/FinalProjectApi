using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        private readonly IFileService _fileService;
        public SliderService(ISliderRepository sliderRepository, IMapper mapper, IFileService fileService)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task CreateAsync(SliderCreateDto model)
        {
            var imagePath = await _fileService.UploadFilesAsync(model.Image, "UploadFiles");
            var slider = _mapper.Map<Slider>(model); 
            slider.Img = imagePath;
            await _sliderRepository.CreateAsync(slider);
        }


        public async Task DeleteAsync(int id)
        {
            var slider = await _sliderRepository.GetWithExpressionAsync(x => x.Id == id);
            if (slider == null) throw new Exception("Slider not found");
            _fileService.Delete(slider.Img, "UploadFiles");
            await _sliderRepository.DeleteAsync(slider);
        }

        public async Task EditAsync(int id, SliderEditDto model)
        {
            var slider = await _sliderRepository.GetByIdWithIncludesAsync(id);
            if (slider == null)
                throw new Exception($"Slider with ID {id} not found");

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(slider.Img))
                    _fileService.Delete(slider.Img, "UploadFiles");

                var imagePath = await _fileService.UploadFilesAsync(model.Image, "UploadFiles");
                slider.Img = imagePath;
            }

            _mapper.Map(model, slider);

            await _sliderRepository.EditAsync(slider);
        }



        public async Task<IEnumerable<SliderDto>> GetAllAsync()
        {
            var sliders = await _sliderRepository.GetAllAsync();
            var sliderDtos = _mapper.Map<IEnumerable<SliderDto>>(sliders);
            return sliderDtos;
        }

        public async Task<SliderDto> GetByIdAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider == null) return null;
            return _mapper.Map<SliderDto>(slider);
        }



    }
}
