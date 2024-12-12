using MediatR;
using Resturant_Api_Reposatry.CQRS.Queries;
using Resturant_Api_Core.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.CQRS.Handlers
{
    public class GetQueriresHandler<T> : IRequestHandler<GetAllQueries<T>, IReadOnlyList<T>> where T : class
    {
        private readonly IUnitOfWork.IUnitOfWork  unitOfWork;

        public GetQueriresHandler(IUnitOfWork.IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<T>> Handle(GetAllQueries<T> request, CancellationToken cancellationToken)
        {
           return await unitOfWork.Repository<T>().GetAllAsync();
        }
    }
}
