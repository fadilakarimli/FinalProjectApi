using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.AboutBlog;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutBlogService : IAboutBlogService
    {
        private readonly IAboutBlogRepository _aboutBlogRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryManager _cloudinaryManager;
        public AboutBlogService(IAboutBlogRepository aboutBlogRepository , IMapper mapper , ICloudinaryManager cloudinaryManager)
        {
            _aboutBlogRepository = aboutBlogRepository;
            _mapper = mapper;
            _cloudinaryManager = cloudinaryManager;
            
        }
        public async Task CreateAsync(AboutBlogCreateDto model)
        {
            string imageUrl = await _cloudinaryManager.FileCreateAsync(model.Image);
            var aboutBlog = _mapper.Map<AboutBlog>(model);
            aboutBlog.Image = imageUrl;

            await _aboutBlogRepository.CreateAsync(aboutBlog);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _aboutBlogRepository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About Blog tapılmadı.");

            await _cloudinaryManager.FileDeleteAsync(entity.Image);
            await _aboutBlogRepository.DeleteAsync(entity);
        }

        public async Task EditAsync(int id, AboutBlogEditDto model)
        {
            var entity = await _aboutBlogRepository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About Blog tapılmadı.");

            if (model.Image != null)
            {
                await _cloudinaryManager.FileDeleteAsync(entity.Image);
                entity.Image = await _cloudinaryManager.FileCreateAsync(model.Image);
            }

            _mapper.Map(model, entity);

            await _aboutBlogRepository.EditAsync(entity);
        }

        public async Task<IEnumerable<AboutBlogDto>> GetAllAsync()
        {
            var entities = await _aboutBlogRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<AboutBlogDto>>(entities);
            return dtos;
        }

        public async Task<AboutBlogDto> GetByIdAsync(int id)
        {
            var entity = await _aboutBlogRepository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("About Blog tapılmadı.");

            var dto = _mapper.Map<AboutBlogDto>(entity);
            return dto;
        }
    }
}
