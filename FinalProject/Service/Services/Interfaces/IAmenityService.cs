using Service.DTOs.Activity;
using Service.DTOs.Amenity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAmenityService
    {
        Task<IEnumerable<AmenityDto>> GetAllAsync();
        Task<AmenityDto> GetByIdAsync(int id);
        Task CreateAsync(AmenityCreateDto model);
        Task EditAsync(int id, AmenityEditDto model);
        Task DeleteAsync(int id);
    }
}
