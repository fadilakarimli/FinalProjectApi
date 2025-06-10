using Service.DTOs.Slider;
using Service.DTOs.Tour;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<TourDto>> GetAllAsync();
        Task<TourDto>GetByIdAsync(int id);
        Task CreateAsync(TourCreateDto model);
        Task EditAsync(int id, TourEditDto model);
        Task DeleteAsync(int id);
        Task<Paginate<TourDto>> GetPaginatedAsync(int page, int pageSize);
        Task<IEnumerable<TourDto>> SearchAsync(TourSearchDto request);
        Task<IEnumerable<TourDto>> SearchByNameAsync(string search);

        //Task<IEnumerable<TourDto>> SortAsync(string sortOrder);
        //Task<Paginate<TourDto>> GetPaginatedDatasAsync(int page);
    }
}
