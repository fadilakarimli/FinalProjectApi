using Service.DTOs.AboutAgency;
using Service.DTOs.ChooseUsAbout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IChooseUsAboutService
    {
        Task<IEnumerable<ChooseUsAboutDto>> GetAllAsync();
        Task<ChooseUsAboutDto> GetByIdAsync(int id);
        Task CreateAsync(ChooseUsAboutCreateDto model);
        Task EditAsync(int id, ChooseUsAboutEditDto model);
        Task DeleteAsync(int id);
    }
}
