using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.CQRS.Commands
{
    public record InsertAllCommands<T>(T t)  : IRequest<T>;
   
}
