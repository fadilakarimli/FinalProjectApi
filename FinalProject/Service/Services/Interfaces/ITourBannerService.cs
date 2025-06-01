using Service.DTOs.AboutAgency;
using Service.DTOs.TourBanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITourBannerService
    {
        Task<IEnumerable<TourBannerDto>> GetAllAsync();
        Task<TourBannerDto> GetByIdAsync(int id);
        Task CreateAsync(TourBannerCreateDto model);
        Task EditAsync(int id, TourBannerEditDto model);
        Task DeleteAsync(int id);
    }
}
