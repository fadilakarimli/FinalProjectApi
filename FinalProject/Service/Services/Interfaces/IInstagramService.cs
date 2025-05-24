using Service.DTOs.Country;
using Service.DTOs.Instagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IInstagramService
    {
        Task CreateAsync(InstagramCreateDto model);
        Task<IEnumerable<InstagramDto>> GetAllAsync();
        Task<InstagramDto> GetByIdAsync(int id);
        Task EditAsync(int id, InstagramEditDto model);
        Task DeleteAsync(int id);
    }
}
