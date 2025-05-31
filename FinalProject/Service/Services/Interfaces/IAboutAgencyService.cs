using Service.DTOs.AboutAgency;
using Service.DTOs.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutAgencyService
    {
        Task<IEnumerable<AboutAgencyDto>> GetAllAsync();
        Task<AboutAgencyDto> GetByIdAsync(int id);
        Task CreateAsync(AboutAgencyCreateDto model);
        Task EditAsync(int id, AboutAgencyEditDto model);
        Task DeleteAsync(int id);
    }
}
