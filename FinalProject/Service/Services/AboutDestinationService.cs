using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.AboutDestination;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutDestinationService : IAboutDestinationService
    {
        private readonly IAboutDestinationRepository _aboutDestinationRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        public AboutDestinationService(IAboutDestinationRepository aboutDestinationRepository , IMapper mapper , ICloudinaryManager cloudinaryManager)
        {
            _aboutDestinationRepository = aboutDestinationRepository;
            _mapper = mapper;
            _cloudinaryManager = cloudinaryManager;
            
        }

        public async Task CreateAsync(AboutDestinationCreateDto model)
        {
            string fileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var aboutDestination = _mapper.Map<AboutDestination>(model);
            aboutDestination.Image = fileUrl;

            await _aboutDestinationRepository.CreateAsync(aboutDestination);
        }

        public async Task DeleteAsync(int id)
        {
            var aboutDestination = await _aboutDestinationRepository.GetByIdAsync(id);
            if (aboutDestination == null)
                throw new Exception("AboutDestination tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(aboutDestination.Image);
            await _aboutDestinationRepository.DeleteAsync(aboutDestination);
        }

        public async Task EditAsync(int id, AboutDestinationEditDto model)
        {
            var existDestination = await _aboutDestinationRepository.GetByIdAsync(id);
            if (existDestination == null)
                throw new Exception("AboutDestination tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existDestination.Image);
                string newFileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                existDestination.Image = newFileUrl;
            }

            _mapper.Map(model, existDestination);

            await _aboutDestinationRepository.EditAsync(existDestination);
        }

        public async Task<IEnumerable<AboutDestinationDto>> GetAllAsync()
        {
            var destinations = await _aboutDestinationRepository.GetAllAsync();
            return _mapper.Map<List<AboutDestinationDto>>(destinations);
        }

        public async Task<AboutDestinationDto> GetByIdAsync(int id)
        {
            var destination = await _aboutDestinationRepository.GetByIdAsync(id);
            if (destination == null)
                return null;

            return _mapper.Map<AboutDestinationDto>(destination);
        }
    }
}
