using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.TourBanner;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TourBannerService : ITourBannerService
    {
        private readonly IMapper _mapper;
        private readonly ITourBannerRepository _tourBannerRepo;
        private readonly ICloudinaryManager _cloudinaryManager;
        public TourBannerService(IMapper mapper , ITourBannerRepository tourBanneRepo, ICloudinaryManager cloudinaryManager )
        {
            _cloudinaryManager = cloudinaryManager;
            _mapper = mapper;
            _tourBannerRepo  = tourBanneRepo;
        }
        public async Task CreateAsync(TourBannerCreateDto model)
        {
            string fileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var tourBanner = _mapper.Map<TourBanner>(model);
            tourBanner.Image = fileUrl;
            await _tourBannerRepo.CreateAsync(tourBanner);
        }
        public async Task DeleteAsync(int id)
        {
            var tourBanner = await _tourBannerRepo.GetByIdAsync(id);
            if (tourBanner == null) throw new Exception("TourBanner tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(tourBanner.Image);
            await _tourBannerRepo.DeleteAsync(tourBanner);
        }
        public async Task EditAsync(int id, TourBannerEditDto model)
        {
            var existBanner = await _tourBannerRepo.GetByIdAsync(id);
            if (existBanner == null) throw new Exception("TourBanner tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existBanner.Image);
                string newFileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                existBanner.Image = newFileUrl;
            }

            _mapper.Map(model, existBanner);

            await _tourBannerRepo.EditAsync(existBanner);
        }

        public async Task<IEnumerable<TourBannerDto>> GetAllAsync()
        {
            var banners = await _tourBannerRepo.GetAllAsync();
            return _mapper.Map<List<TourBannerDto>>(banners);
        }
        public async Task<TourBannerDto> GetByIdAsync(int id)
        {
            var banner = await _tourBannerRepo.GetByIdAsync(id);
            if (banner == null) return null;
            return _mapper.Map<TourBannerDto>(banner);
        }
    }
}
