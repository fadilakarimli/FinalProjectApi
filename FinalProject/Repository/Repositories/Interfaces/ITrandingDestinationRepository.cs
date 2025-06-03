using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ITrandingDestinationRepository : IBaseRepository<TrandingDestination>
    {
        Task<IEnumerable<TrandingDestination>> SortAsync(string sortOrder);

        Task<IEnumerable<TrandingDestination>> GetPaginatedDatasAsync(int page, int take);

        Task<int> GetCountAsync();
    }
}
