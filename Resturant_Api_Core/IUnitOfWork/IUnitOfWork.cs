using Resturant_Api_Core.Reposatries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.IUnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricReposatry<T> Repository<T>() where T : class;
        Task<int> Complete();
    }
}
