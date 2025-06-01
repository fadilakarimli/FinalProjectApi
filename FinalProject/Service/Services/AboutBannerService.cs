using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.AboutBanner;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutBannerService : IAboutBannerService
    {
        private readonly IMapper _mapper;
        private readonly IAboutBannerRepository _aboutBannerRepo;
        private readonly ICloudinaryManager _cloudinaryManager;

        public AboutBannerService(IMapper mapper , IAboutBannerRepository aboutBannerRepo , ICloudinaryManager cloudinaryManager)
        {
            _aboutBannerRepo = aboutBannerRepo;
            _mapper = mapper;
            _cloudinaryManager = cloudinaryManager;
        }
        public async Task CreateAsync(AboutBannerCreateDto model)
        {
            string fileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var aboutBanner = _mapper.Map<AboutBanner>(model);
            aboutBanner.Image = fileUrl;
            await _aboutBannerRepo.CreateAsync(aboutBanner);
        }


        public async Task DeleteAsync(int id)
        {
            var aboutBanner = await _aboutBannerRepo.GetByIdAsync(id);
            if (aboutBanner == null)
                throw new Exception("AboutBanner tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(aboutBanner.Image);
            await _aboutBannerRepo.DeleteAsync(aboutBanner);
        }
        public async Task EditAsync(int id, AboutBannerEditDto model)
        {
            var existBanner = await _aboutBannerRepo.GetByIdAsync(id);
            if (existBanner == null)
                throw new Exception("AboutBanner tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existBanner.Image);
                string newFileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                existBanner.Image = newFileUrl;
            }

            _mapper.Map(model, existBanner);

            await _aboutBannerRepo.EditAsync(existBanner);
        }
        public async Task<IEnumerable<AboutBannerDto>> GetAllAsync()
        {
            var banners = await _aboutBannerRepo.GetAllAsync();
            return _mapper.Map<List<AboutBannerDto>>(banners);
        }

        public async Task<AboutBannerDto> GetByIdAsync(int id)
        {
            var banner = await _aboutBannerRepo.GetByIdAsync(id);
            if (banner == null) return null;
            return _mapper.Map<AboutBannerDto>(banner);
        }
    }
}
