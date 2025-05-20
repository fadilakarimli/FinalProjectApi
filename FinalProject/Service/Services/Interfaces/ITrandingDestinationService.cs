using Service.DTOs.Slider;
using Service.DTOs.TrandingDestination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITrandingDestinationService
    {
        Task CreateAsync(TrandingDestinationCreateDto model);
        Task<IEnumerable<TrandingDestinationDto>> GetAllAsync();
        Task<TrandingDestinationDto> GetByIdAsync(int id);
        Task EditAsync(int id, TrandingDestinationEditDto model);
        Task DeleteAsync(int id);
    }
}
