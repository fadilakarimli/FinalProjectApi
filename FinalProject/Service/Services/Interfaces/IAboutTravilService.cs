using Service.DTOs.AboutTravil;
using Service.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutTravilService
    {
        Task CreateAsync(AboutTravilCreateDto model);
        Task<IEnumerable<AboutTravilDto>> GetAllAsync();
        Task<AboutTravilDto> GetByIdAsync(int id);
        Task EditAsync(int id, AboutTravilEditDto dto);
        Task DeleteAsync(int id);
    }
}
