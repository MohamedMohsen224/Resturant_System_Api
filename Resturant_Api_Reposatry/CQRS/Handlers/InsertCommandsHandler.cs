using MediatR;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Reposatry.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.CQRS.Handlers
{
    public class InsertCommandsHandler<T> : IRequestHandler<InsertAllCommands<T>, T> where T : class
    {
        private readonly IUnitOfWork unitOfWork;

        public InsertCommandsHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<T> Handle(InsertAllCommands<T> request, CancellationToken cancellationToken)
        {
            await unitOfWork.Repository<T>().AddAsync(request.t);
            await unitOfWork.Complete();
            return request.t;
        }
    }
}
