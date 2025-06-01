using Service.DTOs.AboutAgency;
using Service.DTOs.AboutBanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutBannerService
    {
        Task<IEnumerable<AboutBannerDto>> GetAllAsync();
        Task<AboutBannerDto> GetByIdAsync(int id);
        Task CreateAsync(AboutBannerCreateDto model);
        Task EditAsync(int id, AboutBannerEditDto model);
        Task DeleteAsync(int id);
    }
}
