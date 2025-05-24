    using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Brand;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ICloudinaryManager _cloudinaryManager;
        public BrandService(IBrandRepository brandRepo, IMapper mapper, IFileService fileService, ICloudinaryManager cloudinaryManager)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
            _fileService = fileService;
            _cloudinaryManager = cloudinaryManager;
        }

        public async Task CreateAsync(BrandCreateDto model)
        {
            var imagePath = await _cloudinaryManager.FileCreateAsync(model.Image);
            Brand brand = _mapper.Map<Brand>(model);
            brand.Image = imagePath;
            await _brandRepo.CreateAsync(brand);
        }


        public async Task DeleteAsync(int id)
        {
            var brand = await _brandRepo.GetWithExpressionAsync(x=>x.Id ==id);
            if (brand == null) throw new Exception("Brand tapılmadı");
            await _cloudinaryManager.FileDeleteAsync(brand.Image);
            await _brandRepo.DeleteAsync(brand);
        }
                
        public async Task EditAsync(int id, BrandEditDto dto)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null) throw new Exception("Brand tapılmadı");

            if (dto.Image != null)
            {
                _fileService.Delete(brand.Image, "UploadFiles");
                string newImagePath = await _fileService.UploadFilesAsync(dto.Image, "UploadFiles");
                brand.Image = newImagePath;
            }
            await _brandRepo.EditAsync(brand);
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brands = await _brandRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _brandRepo.GetWithExpressionAsync(x=>x.Id==id);
            if (brand == null) throw new Exception("Brand tapılmadı");
            return _mapper.Map<BrandDto>(brand);
        }
    }
}
