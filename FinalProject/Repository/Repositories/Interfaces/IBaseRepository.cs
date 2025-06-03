using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task EditAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetWithExpressionAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllWithExpressionAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllWithIcludesAsync(params Expression<Func<T, object>>[] includes);
        Task<bool> CheckDataWithExpression(Expression<Func<T, bool>> predicate);

    }
}
