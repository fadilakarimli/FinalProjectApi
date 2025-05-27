using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.TrandingDestination;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TrandingDestinationService : ITrandingDestinationService
    {
        private readonly ITrandingDestinationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ICloudinaryManager _cloudinaryManager;
        public TrandingDestinationService(ITrandingDestinationRepository repository , IMapper mapper, IFileService fileService
                                         , ICloudinaryManager cloudinaryManager)
        {
            _repository = repository;   
            _mapper = mapper;
            _fileService = fileService; 
            _cloudinaryManager = cloudinaryManager;
        }
        public async Task CreateAsync(TrandingDestinationCreateDto model)
        {
            string imagePath = await _cloudinaryManager.FileCreateAsync(model.Image);

            var entity = _mapper.Map<TrandingDestination>(model);
            entity.Image = imagePath;

            await _repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _repository.GetWithExpressionAsync(x => x.Id == id);
            if (member == null) throw new Exception("Team member not found");

            if (!string.IsNullOrEmpty(member.Image))
                await _cloudinaryManager.FileDeleteAsync(member.Image);

            await _repository.DeleteAsync(member);
        }

        public async Task EditAsync(int id, TrandingDestinationEditDto model)
        {
            var entity = await _repository.GetWithExpressionAsync(x => x.Id == id);
            if (entity == null) throw new Exception("Tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(entity.Image);
                string newPath = await _cloudinaryManager.FileCreateAsync(model.Image);
                entity.Image = newPath;
            }

            _mapper.Map(model, entity);
            await _repository.EditAsync(entity);
        }

        public async Task<IEnumerable<TrandingDestinationDto>> GetAllAsync()
        {
            var destinations = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TrandingDestinationDto>>(destinations);
        }


        public async Task<TrandingDestinationDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Tranding Destination tapılmadı");

            return _mapper.Map<TrandingDestinationDto>(entity);
        }
    }
}
