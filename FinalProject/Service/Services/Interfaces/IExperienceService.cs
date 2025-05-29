using Service.DTOs.Experience;
using Service.DTOs.Instagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IExperienceService
    {
        Task CreateAsync(ExperienceCreateDto model);
        Task<IEnumerable<ExperienceDto>> GetAllAsync();
        Task<ExperienceDto> GetByIdAsync(int id);
        Task EditAsync(int id, ExperienceEditDto model);
        Task DeleteAsync(int id);
    }
}
