using Service.DTOs.AboutAgency;
using Service.DTOs.AboutApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutAppService
    {
        Task<IEnumerable<AboutAppDto>> GetAllAsync();
        Task<AboutAppDto> GetByIdAsync(int id);
        Task CreateAsync(AboutAppCreateDto model);
        Task EditAsync(int id, AboutAppEditDto model);
        Task DeleteAsync(int id);
    }
}
