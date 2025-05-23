using Service.DTOs.Blog;
using Service.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBlogService
    {
        Task CreateAsync(BlogCreateDto model);
        Task<IEnumerable<BlogDto>> GetAllAsync();
        Task<BlogDto> GetByIdAsync(int id);
        Task EditAsync(int id, BlogEditDto model);
        Task DeleteAsync(int id);
    }
}
