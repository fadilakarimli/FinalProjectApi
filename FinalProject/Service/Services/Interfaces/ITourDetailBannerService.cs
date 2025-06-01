using Service.DTOs.TourBanner;
using Service.DTOs.TourDetailBanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITourDetailBannerService
    {
        Task<IEnumerable<TourDetailBannerDto>> GetAllAsync();
        Task<TourDetailBannerDto> GetByIdAsync(int id);
        Task CreateAsync(TourDetailBannerCreateDto model);
        Task EditAsync(int id, TourDetailBannerEditDto model);
        Task DeleteAsync(int id);
    }
}
