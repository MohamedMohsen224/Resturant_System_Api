using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.CQRS.Queries
{
    public record GetAllQueries<T> : IRequest<IReadOnlyList<T>>;
    
}
