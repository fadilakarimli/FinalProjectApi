using Service.DTOs.Brand;
using Service.DTOs.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICityService
    {
        Task CreateAsync(CityCreateDto request);
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<CityDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, CityEditDto request);
    }
}
