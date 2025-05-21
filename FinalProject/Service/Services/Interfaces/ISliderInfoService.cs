using Service.DTOs.SliderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task CreateAsync(SliderInfoCreateDto model);
        Task EditAsync(int id, SliderInfoEditDto model);
        Task DeleteAsync(int id);
        Task<SliderInfoDto> GetByIdAsync(int id);
        Task<IEnumerable<SliderInfoDto>> GetAllAsync();
    }
}
