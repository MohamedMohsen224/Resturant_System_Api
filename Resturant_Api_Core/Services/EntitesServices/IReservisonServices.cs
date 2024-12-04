using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Specification.ReservisonSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Services.EntitesServices
{
    public interface IReservisonServices
    {
        Task<IReadOnlyList<Reservison>> GetAllReservisons(ResrveParms parms);
        Task<Reservison> GetById(int id);
    }
}
