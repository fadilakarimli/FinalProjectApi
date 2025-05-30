using Service.DTOs.DestinationFeature;
using Service.DTOs.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IPlanService
    {
        Task CreateAsync(PlanCreateDto model);
        Task<IEnumerable<PlanDto>> GetAllAsync();
        Task<PlanDto> GetByIdAsync(int id);
        Task EditAsync(int id, PlanEditDto dto);
        Task DeleteAsync(int id);
    }
}
