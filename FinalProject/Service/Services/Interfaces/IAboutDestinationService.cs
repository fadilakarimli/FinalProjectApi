using Service.DTOs.AboutBanner;
using Service.DTOs.AboutDestination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutDestinationService
    {
        Task<IEnumerable<AboutDestinationDto>> GetAllAsync();
        Task<AboutDestinationDto> GetByIdAsync(int id);
        Task CreateAsync(AboutDestinationCreateDto model);
        Task EditAsync(int id, AboutDestinationEditDto model);
        Task DeleteAsync(int id);
    }
}
