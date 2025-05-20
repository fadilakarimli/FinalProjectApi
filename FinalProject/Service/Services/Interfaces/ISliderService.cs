using Service.DTOs.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISliderService
    {
        Task CreateAsync(SliderCreateDto model);
        Task<IEnumerable<SliderDto>> GetAllAsync();
        Task<SliderDto> GetByIdAsync(int id);
        Task EditAsync(int id, SliderEditDto model);
        Task DeleteAsync(int id);
    }
}
