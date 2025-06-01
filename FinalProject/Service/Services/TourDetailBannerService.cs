using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.TourDetailBanner;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TourDetailBannerService : ITourDetailBannerService
    {
        private readonly IMapper _mapper;
        private readonly ITourDetailBannerRepository _tourDetailBannerRepo;
        private readonly ICloudinaryManager _cloudinaryManager;
        public TourDetailBannerService(IMapper mapper , ITourDetailBannerRepository tourDetailBannerService , ICloudinaryManager cloudinaryManager)
        {
            _cloudinaryManager = cloudinaryManager;
            _mapper = mapper;
            _tourDetailBannerRepo = tourDetailBannerService;

        }
        public async Task CreateAsync(TourDetailBannerCreateDto model)
        {
            string fileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var tourDetailBanner = _mapper.Map<TourDetailBanner>(model);
            tourDetailBanner.Image = fileUrl;
            await _tourDetailBannerRepo.CreateAsync(tourDetailBanner);
        }

        public async Task DeleteAsync(int id)
        {
            var tourDetailBanner = await _tourDetailBannerRepo.GetByIdAsync(id);
            if (tourDetailBanner == null) throw new Exception("TourDetailBanner tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(tourDetailBanner.Image);
            await _tourDetailBannerRepo.DeleteAsync(tourDetailBanner);
        }

        public async Task EditAsync(int id, TourDetailBannerEditDto model)
        {
            var existBanner = await _tourDetailBannerRepo.GetByIdAsync(id);
            if (existBanner == null) throw new Exception("TourDetailBanner tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existBanner.Image);
                string newFileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                existBanner.Image = newFileUrl;
            }

            _mapper.Map(model, existBanner);

            await _tourDetailBannerRepo.EditAsync(existBanner);
        }

        public async Task<IEnumerable<TourDetailBannerDto>> GetAllAsync()
        {
            var banners = await _tourDetailBannerRepo.GetAllAsync();
            return _mapper.Map<List<TourDetailBannerDto>>(banners);
        }

        public async Task<TourDetailBannerDto> GetByIdAsync(int id)
        {
            var banner = await _tourDetailBannerRepo.GetByIdAsync(id);
            if (banner == null) return null;
            return _mapper.Map<TourDetailBannerDto>(banner);
        }
    }
}
