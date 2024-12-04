using Resturant_Api_Core.Entites;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.ReservisonSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Services.EntitesSERVICES
{
    public class ReservisonServices : IReservisonServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ReservisonServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Reservison>> GetAllReservisons(ResrveParms parms)
        {
            var Spec = new ReserveSpec(parms);
            var Resrvisons = await unitOfWork.Repository<Reservison>().GetAllWithSpecAsync(Spec);
            return Resrvisons;
        }

        public async Task<Reservison> GetById(int id)
        {
            var Spec = new ReserveSpec(id);
            var Resrcison = await unitOfWork.Repository<Reservison>().GetByIdWithSpecAsync(Spec);
            if (Resrcison == null)
                throw new Exception("No Reservison with this Id");
            return Resrcison;
        }
    }
}
