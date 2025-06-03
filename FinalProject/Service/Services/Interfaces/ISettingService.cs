using Service.DTOs.Setting;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISettingService
    {
        Task<DataOperationResponse> CreateAsync(SettingCreateDto setting);
        Task<DataOperationResponse> DeleteAsync(int id);
        Task<DataOperationResponse> EditAsync(int id, SettingEditDto setting);
        Task<IEnumerable<SettingDto>> GetAllAsync();
        Task<SettingDto> GetByIdAsync(int id);
    }
}
