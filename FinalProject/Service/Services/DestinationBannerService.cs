using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.DestinationBanner;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DestinationBannerService : IDestinationBannerService
    {
        private readonly IDestinationBannerRepository _destinationBannerRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        public DestinationBannerService(IDestinationBannerRepository destinationBannerRepository , IMapper mapper , ICloudinaryManager cloudinaryManager)
        {
            _cloudinaryManager = cloudinaryManager;
            _destinationBannerRepository = destinationBannerRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(DestinationBannerCreateDto model)
        {
            string fileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var destinationBanner = _mapper.Map<DestinationBanner>(model);
            destinationBanner.Image = fileUrl;
            await _destinationBannerRepository.CreateAsync(destinationBanner);
        }

        public async Task DeleteAsync(int id)
        {
            var destinationBanner = await _destinationBannerRepository.GetByIdAsync(id);
            if (destinationBanner == null)
                throw new Exception("DestinationBanner tapılmadı");

            await _cloudinaryManager.FileDeleteAsync(destinationBanner.Image);
            await _destinationBannerRepository.DeleteAsync(destinationBanner);
        }

        public async Task EditAsync(int id, DestinationBannerEditDto model)
        {
            var existBanner = await _destinationBannerRepository.GetByIdAsync(id);
            if (existBanner == null)
                throw new Exception("DestinationBanner tapılmadı");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(existBanner.Image);
                string newFileUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                existBanner.Image = newFileUrl;
            }

            _mapper.Map(model, existBanner);

            await _destinationBannerRepository.EditAsync(existBanner);
        }

        public async Task<IEnumerable<DestinationBannerDto>> GetAllAsync()
        {
            var banners = await _destinationBannerRepository.GetAllAsync();
            return _mapper.Map<List<DestinationBannerDto>>(banners);
        }

        public async Task<DestinationBannerDto> GetByIdAsync(int id)
        {
            var banner = await _destinationBannerRepository.GetByIdAsync(id);
            if (banner == null) return null;
            return _mapper.Map<DestinationBannerDto>(banner);
        }
    }
}
