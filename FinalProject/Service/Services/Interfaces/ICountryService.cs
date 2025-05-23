using Service.DTOs.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICountryService
    {
        Task CreateAsync(CountryCreateDto request);
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task<CountryDto> GetByIdAsync(int id);
        Task EditAsync(int id, CountryEditDto request);
        Task DeleteAsync(int id);
    }
}
