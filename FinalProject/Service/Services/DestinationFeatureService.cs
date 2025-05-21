using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Brand;
using Service.DTOs.DestinationFeature;
using Service.DTOs.Slider;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DestinationFeatureService : IDestinationFeatureService
    {
        private readonly IDestinationFeatureRepository _destinationFeatureRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public DestinationFeatureService(IDestinationFeatureRepository destinationFeatureRepository, IMapper mapper, IFileService fileService)
        {
            _destinationFeatureRepository = destinationFeatureRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task CreateAsync(DestinationFeatureCreateDto model)
        {
            var imagePath = await _fileService.UploadFilesAsync(model.IconImage, "UploadFiles");
            var slider = _mapper.Map<DestinationFeature>(model);
            slider.IconImage = imagePath;
            await _destinationFeatureRepository.CreateAsync(slider);
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _destinationFeatureRepository.GetWithExpressionAsync(x => x.Id == id);
            if (slider == null) throw new Exception("Destination not found");
            _fileService.Delete(slider.IconImage, "UploadFiles");
            await _destinationFeatureRepository.DeleteAsync(slider);
        }

        public Task EditAsync(int id, DestinationFeatureEditDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DestinationFeatureDto>> GetAllAsync()
        {
            var destination = await _destinationFeatureRepository.GetAllAsync();
            var destinationDtos = _mapper.Map<IEnumerable<DestinationFeatureDto>>(destination);
            return destinationDtos;
        }

        public async  Task<DestinationFeatureDto> GetByIdAsync(int id)
        {
            var destination = await _destinationFeatureRepository.GetByIdAsync(id);
            if (destination == null) return null;
            return _mapper.Map<DestinationFeatureDto>(destination);
        }
    }
}
