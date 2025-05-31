using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.AboutAgency;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutAgencyService : IAboutAgencyService
    {
        private readonly IAboutAgencyRepository _aboutAgencyRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        public AboutAgencyService(IAboutAgencyRepository aboutAgencyRepository , IMapper mapper , ICloudinaryManager cloudinaryManager)
        {
            _aboutAgencyRepository = aboutAgencyRepository;
            _mapper = mapper;
            _cloudinaryManager = cloudinaryManager;
        }
        public async Task CreateAsync(AboutAgencyCreateDto model)
        {
            string imageUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var aboutAgency = _mapper.Map<AboutAgency>(model);
            aboutAgency.Image = imageUrl;

            await _aboutAgencyRepository.CreateAsync(aboutAgency);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _aboutAgencyRepository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About Agency tapılmadı.");
            await _cloudinaryManager.FileDeleteAsync(entity.Image);
            await _aboutAgencyRepository.DeleteAsync(entity);
        }

        public async Task EditAsync(int id, AboutAgencyEditDto model)
        {
            var entity = await _aboutAgencyRepository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About Agency tapılmadı.");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(entity.Image);
                entity.Image = await _cloudinaryManager.FileCreateAsync(model.Image);
            }

            _mapper.Map(model, entity);

            await _aboutAgencyRepository.EditAsync(entity);
        }


        public async Task<IEnumerable<AboutAgencyDto>> GetAllAsync()
        {
            var entities = await _aboutAgencyRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<AboutAgencyDto>>(entities);
            return dtos;
        }

        public async Task<AboutAgencyDto> GetByIdAsync(int id)
        {
            var entity = await _aboutAgencyRepository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About Agency tapılmadı.");
            var dto = _mapper.Map<AboutAgencyDto>(entity);
            return dto;
        }
    }
}
