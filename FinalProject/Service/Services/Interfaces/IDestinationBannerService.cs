using Domain.Entities;
using Service.DTOs.DestinationBanner;
using Service.DTOs.TourDetailBanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IDestinationBannerService
    {
        Task<IEnumerable<DestinationBannerDto>> GetAllAsync();
        Task<DestinationBannerDto> GetByIdAsync(int id);
        Task CreateAsync(DestinationBannerCreateDto model);
        Task EditAsync(int id, DestinationBannerEditDto model);
        Task DeleteAsync(int id);
    }
}
