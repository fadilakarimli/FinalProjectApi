using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Brand;
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
        private readonly ICloudinaryManager _cloudinaryManager;
        public SliderService(ISliderRepository sliderRepository, IMapper mapper, IFileService fileService,
                            ICloudinaryManager cloudinaryManager)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
            _fileService = fileService;
            _cloudinaryManager = cloudinaryManager;
        }

        public async Task CreateAsync(SliderCreateDto model)
        {
            var imagePath = await _cloudinaryManager.FileCreateAsync(model.Image);
            Slider slider = _mapper.Map<Slider>(model);
            slider.Img = imagePath;
            await _sliderRepository.CreateAsync(slider);
        }
        public async Task DeleteAsync(int id)
        {
            var slider = await _sliderRepository.GetWithExpressionAsync(x => x.Id == id);
            if (slider == null) throw new Exception("Slider tapılmadı");
            await _cloudinaryManager.FileDeleteAsync(slider.Img);
            await _sliderRepository.DeleteAsync(slider);
        }


        public async Task EditAsync(int id, SliderEditDto model)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider == null)
                throw new Exception("Brand tapılmadı");

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(slider.Img))
                {
                    bool deleted = await _cloudinaryManager.FileDeleteAsync(slider.Img);
                }
                string newImagePath = await _cloudinaryManager.FileCreateAsync(model.Image);
                slider.Img = newImagePath;
            }
            await _sliderRepository.EditAsync(slider);
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
