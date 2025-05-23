using Service.DTOs.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDto>> GetAllAsync();
        Task<ActivityDto> GetByIdAsync(int id);
        Task CreateAsync(ActivityCreateDto model);
        Task EditAsync(int id, ActivityEditDto model);
        Task DeleteAsync(int id);
    }
}
