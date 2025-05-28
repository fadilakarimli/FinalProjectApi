using Service.DTOs.Slider;
using Service.DTOs.SpecialOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISpecialOfferModelService
    {
        Task<IEnumerable<SpecialOfferDto>> GetAllAsync();
        Task<SpecialOfferDto> GetByIdAsync(int id);
        Task CreateAsync(SpecialOfferCreateDto model);
        Task EditAsync(int id, SpecialOfferEditDto model);
        Task DeleteAsync(int id);
    }
}
