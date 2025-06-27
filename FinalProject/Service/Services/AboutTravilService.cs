using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.AboutTravil;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutTravilService : IAboutTravilService
    {
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        private readonly IAboutTravilRepository _aboutTravilRepo;
        public AboutTravilService(IMapper mapper, ICloudinaryManager cloudinaryManager, IAboutTravilRepository aboutTravilRepo)
        {
            _aboutTravilRepo = aboutTravilRepo;
            _mapper = mapper;
            _cloudinaryManager = cloudinaryManager;
        }
        public async Task CreateAsync(AboutTravilCreateDto model)
        {
            string fileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            string smallImageUrl = await _cloudinaryManager.FileCreateAsync(model.SmallImage);
            var aboutTravil = _mapper.Map<AboutTravil>(model);
            aboutTravil.Image = fileUrl;
            aboutTravil.SmallImage = smallImageUrl;
            await _aboutTravilRepo.CreateAsync(aboutTravil);
        }

        public async Task DeleteAsync(int id)
        {
            var aboutTravil = await _aboutTravilRepo.GetByIdAsync(id);
            if (aboutTravil == null)
                throw new Exception("AboutTravil tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(aboutTravil.Image);
            await _aboutTravilRepo.DeleteAsync(aboutTravil);
        }

        public async Task EditAsync(int id, AboutTravilEditDto model)
        {
            var existTravil = await _aboutTravilRepo.GetByIdAsync(id);
            if (existTravil == null)
                throw new Exception("AboutTravil tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existTravil.Image);
                string newFileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                existTravil.Image = newFileUrl;
            }

            _mapper.Map(model, existTravil);
            await _aboutTravilRepo.EditAsync(existTravil);
        }

        public async Task<IEnumerable<AboutTravilDto>> GetAllAsync()
        {
            var travils = await _aboutTravilRepo.GetAllAsync();
            return _mapper.Map<List<AboutTravilDto>>(travils);
        }

        public async Task<AboutTravilDto> GetByIdAsync(int id)
        {
            var travil = await _aboutTravilRepo.GetByIdAsync(id);
            if (travil == null)
                return null;

            return _mapper.Map<AboutTravilDto>(travil);
        }
    }
}
