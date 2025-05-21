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
            string fileName = await _fileService.UploadFilesAsync(model.Image, "UploadFiles");

            var slider = _mapper.Map<Slider>(model);
            slider.Img = fileName;
            await _sliderRepository.CreateAsync(slider);
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider is null) throw new Exception("Slider tapılmadı");
            _fileService.Delete(slider.Img, "UploadFiles");
            await _sliderRepository.DeleteAsync(slider);
        }

        public async Task EditAsync(int id, SliderEditDto model)
        {
            var existSlider = await _sliderRepository.GetByIdAsync(id);
            if (existSlider is null) throw new Exception("Slider tapılmadı");

            if (model.Image != null)
            {
                _fileService.Delete(existSlider.Img, "UploadFiles");
                string newFileName = await _fileService.UploadFilesAsync(model.Image, "UploadFiles");
                existSlider.Img = newFileName;
            }
            _mapper.Map(model, existSlider); 
            await _sliderRepository.EditAsync(existSlider);
        }

        public async Task<IEnumerable<SliderDto>> GetAllAsync()
        {
            var sliders = await _sliderRepository.GetAllAsync();
            return _mapper.Map<List<SliderDto>>(sliders);
        }

        public async Task<SliderDto> GetByIdAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider == null) return null;
            return _mapper.Map<SliderDto>(slider);
        }

    }
}
