using Microsoft.EntityFrameworkCore;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Core.Reposatries;
using Resturant_Api_Reposatry.Data.AppContext;
using Resturant_Api_Reposatry.Reposatries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.UnitOfWork
{
    public class UnitOfWork :IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly ResturantContext _dbContext;


        public UnitOfWork(ResturantContext context)
        {
            _dbContext = context;
            this._repositories = new Hashtable();

        }

        public IGenaricReposatry<T> Repository<T>() where T : class
        {
            var key = typeof(T).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenaricReposatry<T>(_dbContext);

                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenaricReposatry<T>;
        }

        public async Task<int> Complete()
            => await _dbContext.SaveChangesAsync();
       

        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();
    }
}
