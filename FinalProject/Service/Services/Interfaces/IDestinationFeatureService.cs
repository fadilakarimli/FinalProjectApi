using Service.DTOs.Brand;
using Service.DTOs.DestinationFeature;
using Service.DTOs.TrandingDestination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IDestinationFeatureService
    {
        Task CreateAsync(DestinationFeatureCreateDto model);
        Task<IEnumerable<DestinationFeatureDto>> GetAllAsync();
        Task<DestinationFeatureDto> GetByIdAsync(int id);
        Task EditAsync(int id, DestinationFeatureEditDto dto);
        Task DeleteAsync(int id);

    }
}
