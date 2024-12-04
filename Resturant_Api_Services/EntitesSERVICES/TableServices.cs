using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Entites.Enum;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.TableSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Services.EntitesSERVICES
{
    public class TableServices : ITableServices
    {
        private readonly IUnitOfWork unitOfWork;

        public TableServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Table> CreateTable(Table table)
        {
            var tb = await unitOfWork.Repository<Table>().AddAsync(table);
            await unitOfWork.Complete();
            return tb;
            
        }
        public async Task<IReadOnlyList<Table>> GetAllTables(TableParms parms)
        {
            var spec = new TableSpec(parms);
            var tabls = await unitOfWork.Repository<Table>().GetAllWithSpecAsync(spec);
            return tabls;
            
        }
        public async Task<IReadOnlyList<Table>> GetAvalibleTables(TableParms parms)
        {
            var Spec = new TableSpec(parms);
            if (parms.IsAvalible == TableStatus.NotAvalible)
            {
                throw new Exception("No Avalible Tables");
            }
            var AvATables = await unitOfWork.Repository<Table>().GetAllWithSpecAsync(Spec);
            return AvATables;
            
           
        }
        public async Task<Table> GetTableById(int Cap)
        {
            var Spec = new TableSpec(Cap);
            var tb = await unitOfWork.Repository<Table>().GetByIdWithSpecAsync(Spec);
            if (tb == null)
                throw new Exception("No Table with this Capcity ");
            return tb;
        }
        public async Task<Reservison> ReserveTable(Table table)
        {
            if(table.IsAvailable == TableStatus.NotAvalible)
            {
                throw new InvalidOperationException("The table is already reserved.");
            }

            // Create the reservation
            var reservation = new Reservison
            {
                TableId = table.Id,
                Table = table,
                ReservationDate = DateTime.Now,
                ReservationEndTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(23)), // Example end time
                IsCompleted = false
            };

            // Mark the table as not available
            table.IsAvailable = TableStatus.NotAvalible;

            // Update the table and add the reservation
            var tableRepo = unitOfWork.Repository<Table>();
            var reservationRepo = unitOfWork.Repository<Reservison>();

            tableRepo.Update(table);
            await reservationRepo.AddAsync(reservation);

            // Commit the transaction
            await unitOfWork.Complete();

            return reservation;
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

            reservationRepo.Update(reservation);
            await unitOfWork.Complete();
        }
        public async Task MakeTableAvalible(int tableId , int ReservationId)
        {
            var reservationRepo = unitOfWork.Repository<Reservison>();
            var Table = await unitOfWork.Repository<Table>().GetByIdAsync(tableId);
            var reservation = await reservationRepo.GetByIdAsync(ReservationId);
            if (Table == null)
            {
                throw new KeyNotFoundException("Table not found.");
            }
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
            Table.IsAvailable = TableStatus.Avalible;
            reservation.IsCompleted = true;
            unitOfWork.Repository<Reservison>().Delete(reservation);
            await unitOfWork.Complete();

        }
    }
}

