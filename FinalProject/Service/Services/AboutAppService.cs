using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.AboutApp;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutAppService : IAboutAppService
    {
        private readonly IAboutAppRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        public AboutAppService(IAboutAppRepository repository , IMapper mapper , ICloudinaryManager cloudinaryManager)
        {
            _cloudinaryManager = cloudinaryManager; 
            _repository = repository;   
            _mapper = mapper;
        }
        public async Task CreateAsync(AboutAppCreateDto model)
        {
            var aboutApp = _mapper.Map<AboutApp>(model);

            if (model.Image != null)
            {
                aboutApp.Image = await _cloudinaryManager.FileCreateAsync(model.Image);
            }

            if (model.AppleImage != null)
            {
                aboutApp.AppleImage = await _cloudinaryManager.FileCreateAsync(model.AppleImage);
            }

            if (model.PlayStoreImage != null)
            {
                aboutApp.PlayStoreImage = await _cloudinaryManager.FileCreateAsync(model.PlayStoreImage);
            }

            if (model.BackgroundImage != null)
            {
                aboutApp.BackgroundImage = await _cloudinaryManager.FileCreateAsync(model.BackgroundImage);
            }

            await _repository.CreateAsync(aboutApp);
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About App tapılmadı.");

            await _cloudinaryManager.FileDeleteAsync(entity.Image);
            await _repository.DeleteAsync(entity);
        }

        public async Task EditAsync(int id, AboutAppEditDto model)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About App tapılmadı.");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(entity.Image);
                entity.Image = await _cloudinaryManager.FileCreateAsync(model.Image);
            }

            if (model.AppleImage != null)
            {
                await _cloudinaryManager.FileDeleteAsync(entity.AppleImage);
                entity.AppleImage = await _cloudinaryManager.FileCreateAsync(model.AppleImage);
            }

            if (model.PlayStoreImage != null)
            {
                await _cloudinaryManager.FileDeleteAsync(entity.PlayStoreImage);
                entity.PlayStoreImage = await _cloudinaryManager.FileCreateAsync(model.PlayStoreImage);
            }

            if (model.BackgroundImage != null)
            {
                await _cloudinaryManager.FileDeleteAsync(entity.BackgroundImage);
                entity.BackgroundImage = await _cloudinaryManager.FileCreateAsync(model.BackgroundImage);
            }

            _mapper.Map(model, entity);

            await _repository.EditAsync(entity);
        }

        public async Task<IEnumerable<AboutAppDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<AboutAppDto>>(entities);
            return dtos;
        }

        public async Task<AboutAppDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About App tapılmadı.");

            var dto = _mapper.Map<AboutAppDto>(entity);
            return dto;
        }
    }
}
