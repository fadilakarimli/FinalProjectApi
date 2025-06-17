using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ITourRepository : IBaseRepository<Tour>
    {
        Task<IEnumerable<Tour>> GetAllTourWithActivityAsync();
        Task<Tour> GetByIdWithIncludesAsync(int id);
        Task<IEnumerable<Tour>> SortAsync(string sortOrder);
        Task<IEnumerable<Tour>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
        Task<IEnumerable<Tour>> SearchAsync(string city, string activity, DateTime? date, int? guestCount);
        IQueryable<Tour> GetAllWithIncludesQueryable(params Expression<Func<Tour, object>>[] includes);
        Task<IEnumerable<Tour>> GetAllForFilterAsync();


    }
}
