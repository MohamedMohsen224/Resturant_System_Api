using Microsoft.EntityFrameworkCore;
using Resturant_Api_Core.Reposatries;
using Resturant_Api_Reposatry.Data.AppContext;
using Resturant_Api_Reposatry.Reposatries.Evelutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.Reposatries
{
    public class GenaricReposatry<T> : IGenaricReposatry<T> where T : class 
    {
        private readonly ResturantContext context;

        public GenaricReposatry(ResturantContext context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {

            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecEveluator<T>.GetQuery(context.Set<T>(), spec);
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;

        }
        public void Update(T entity)
            => context.Update(entity);
        public void Delete(T entity)
            => context.Remove(entity);

        public async Task<T> GetByIdAsync(int id)
        {
          return await context.FindAsync<T>(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsyncForClean(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = context.Set<T>();

            // Apply the predicate if provided
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
    }
   
}
