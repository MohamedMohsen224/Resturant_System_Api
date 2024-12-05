using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Entites.Enum;
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
        public async Task CompleteReservationAsync(int reservationId)
        {
            var reservationRepo = unitOfWork.Repository<Reservison>();
            var tableRepo = unitOfWork.Repository<Table>();

            var reservation = await reservationRepo.GetByIdAsync(reservationId);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

            reservation.IsCompleted = true;

            // Make the table available again
            var table = await tableRepo.GetByIdAsync(reservation.TableId);
            if (table != null)
            {
                table.IsAvailable = TableStatus.Avalible;
                tableRepo.Update(table);
            }

            reservationRepo.Delete(reservation);
            await unitOfWork.Complete();
        }

    }
}
