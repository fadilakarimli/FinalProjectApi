using Service.DTOs.AboutBanner;
using Service.DTOs.AboutBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutBlogService
    {
        Task<IEnumerable<AboutBlogDto>> GetAllAsync();
        Task<AboutBlogDto> GetByIdAsync(int id);
        Task CreateAsync(AboutBlogCreateDto model);
        Task EditAsync(int id, AboutBlogEditDto model);
        Task DeleteAsync(int id);
    }
}
