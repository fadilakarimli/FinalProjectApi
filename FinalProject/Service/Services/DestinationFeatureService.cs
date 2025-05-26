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
        private readonly ICloudinaryManager _cloudinaryManager;
        public DestinationFeatureService(IDestinationFeatureRepository destinationFeatureRepository, IMapper mapper, IFileService fileService
                                       , ICloudinaryManager cloudinaryManager)
        {
            _destinationFeatureRepository = destinationFeatureRepository;
            _mapper = mapper;
            _fileService = fileService;
            _cloudinaryManager = cloudinaryManager;
        }

        public async Task CreateAsync(DestinationFeatureCreateDto model)
        {
            var imagePath = await _cloudinaryManager.FileCreateAsync(model.IconImage);
            var entity = _mapper.Map<DestinationFeature>(model);
            entity.IconImage = imagePath;
            await _destinationFeatureRepository.CreateAsync(entity);
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _destinationFeatureRepository.GetWithExpressionAsync(x => x.Id == id);
            if (entity == null) throw new Exception("Destination not found");

            await _cloudinaryManager.FileDeleteAsync(entity.IconImage);
            await _destinationFeatureRepository.DeleteAsync(entity);
        }


        public async Task EditAsync(int id, DestinationFeatureEditDto dto)
        {
            var entity = await _destinationFeatureRepository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Destination Feature tapılmadı");

            if (dto.IconImage != null)
            {
                await _cloudinaryManager.FileDeleteAsync(entity.IconImage);
                string newImagePath = await _cloudinaryManager.FileCreateAsync(dto.IconImage);
                entity.IconImage = newImagePath;
            }

            _mapper.Map(dto, entity);

            await _destinationFeatureRepository.EditAsync(entity);
        }

        public async Task<IEnumerable<DestinationFeatureDto>> GetAllAsync()
        {
            var entities = await _destinationFeatureRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DestinationFeatureDto>>(entities);
        }


        public async Task<DestinationFeatureDto> GetByIdAsync(int id)
        {
            var entity = await _destinationFeatureRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<DestinationFeatureDto>(entity);
        }
    }
}
