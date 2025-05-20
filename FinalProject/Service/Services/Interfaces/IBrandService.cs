using Service.DTOs.Brand;
using Service.DTOs.SliderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBrandService
    {
        Task CreateAsync(BrandCreateDto model);
        Task<IEnumerable<BrandDto>> GetAllAsync();
        Task<BrandDto> GetByIdAsync(int id);
        Task EditAsync(int id, BrandEditDto dto);
        Task DeleteAsync(int id);
    }
}
