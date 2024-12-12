using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Reposatries
{
    public interface IGenaricReposatry<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsyncForClean(Expression<Func<T, bool>> predicate = null);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllCqrsAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
