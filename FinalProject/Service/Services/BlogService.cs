using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Blog;
using Service.DTOs.Slider;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public BlogService(IBlogRepository blogRepo , IMapper mapper, IFileService fileService)
        {
            _blogRepo = blogRepo;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task CreateAsync(BlogCreateDto model)
        {
            string fileName = await _fileService.UploadFilesAsync(model.Image, "UploadFiles");
            var blog = _mapper.Map<Blog>(model);
            blog.Image = fileName;
            await _blogRepo.CreateAsync(blog);
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog is null) throw new Exception("Blog tapılmadı");
            _fileService.Delete(blog.Image, "UploadFiles");
            await _blogRepo.DeleteAsync(blog);
        }

        public async Task EditAsync(int id, BlogEditDto model)
        {
            var existBlog = await _blogRepo.GetByIdAsync(id);
            if (existBlog is null) throw new Exception("Blog tapılmadı");

            if (model.Image != null)
            {
                _fileService.Delete(existBlog.Image, "UploadFiles");
                string newFileName = await _fileService.UploadFilesAsync(model.Image, "UploadFiles");
                existBlog.Image = newFileName;
            }
            _mapper.Map(model, existBlog);
            await _blogRepo.EditAsync(existBlog);
        }

        public async Task<IEnumerable<BlogDto>> GetAllAsync()
        {
            var blogs = await _blogRepo.GetAllAsync();
            return _mapper.Map<List<BlogDto>>(blogs);
        }

        public async Task<BlogDto> GetByIdAsync(int id)
        {

            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null) return null;
            return _mapper.Map<BlogDto>(blog);
        }
    }
}
