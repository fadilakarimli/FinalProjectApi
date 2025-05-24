using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Brand;
using Service.DTOs.Instagram;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class InstagramService : IInstagramService
    {
        private readonly IInstagramRepository _instagramRepository;
        private readonly ICloudinaryManager _cloudinaryManager;
        private readonly IMapper _mapper;
        public InstagramService(IInstagramRepository instagramRepository, ICloudinaryManager cloudinaryManager
                                , IMapper mapper)
        {
            _instagramRepository = instagramRepository;
            _cloudinaryManager = cloudinaryManager;
            _mapper = mapper;
        }
        public async Task CreateAsync(InstagramCreateDto model)
        {
            var imagePath = await _cloudinaryManager.FileCreateAsync(model.Image);
            Instagram instagram = _mapper.Map<Instagram>(model);
            instagram.Image = imagePath;
            await _instagramRepository.CreateAsync(instagram);
        }

        public async Task DeleteAsync(int id)
        {
            var insta = await _instagramRepository.GetWithExpressionAsync(x => x.Id == id);
            if (insta == null) throw new Exception("Instagram tapılmadı");
            await _cloudinaryManager.FileDeleteAsync(insta.Image);
            await _instagramRepository.DeleteAsync(insta);
        }
        public async Task EditAsync(int id, InstagramEditDto model)
        {
            var insta = await _instagramRepository.GetByIdAsync(id);
            if (insta == null) throw new Exception("Instagram tapılmadı");

            if (model.Image != null)
            {
                string newImageUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
                if (!string.IsNullOrEmpty(insta.Image))
                {
                    await _cloudinaryManager.FileDeleteAsync(insta.Image);
                }
                insta.Image = newImageUrl;
            }
            await _instagramRepository.EditAsync(insta);
        }

        public async Task<IEnumerable<InstagramDto>> GetAllAsync()
        {
            var insta = await _instagramRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InstagramDto>>(insta);
        }

        public async Task<InstagramDto> GetByIdAsync(int id)
        {
            var insta = await _instagramRepository.GetWithExpressionAsync(x => x.Id == id);
            if (insta == null) throw new Exception("Insta tapılmadı");
            return _mapper.Map<InstagramDto>(insta);
        }
    }
}
