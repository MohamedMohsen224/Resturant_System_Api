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
            await unitOfWork.Repository<Table>().AddAsync(table);
            await unitOfWork.Complete();
            return table;
            
        }
        public async Task<IReadOnlyList<Table>> GetAllTables(TableParms parms)
        {
            var spec = new TableSpec(parms);
            var tabls = await unitOfWork.Repository<Table>().GetAllWithSpecAsync(spec);
            return tabls;
            
        }
        public async Task<IReadOnlyList<Table>> GetAvalibleTables(TableParms parms)
        {
            var spec = new TableSpec(parms); // Set IsAvailable to true
            var availableTables = await unitOfWork.Repository<Table>().GetAllWithSpecAsync(spec);
            return availableTables;


        }
        public async Task<Reservison> ReserveTable(Table table, string reservationName)
        {
            // Retrieve the table from the database
            var Spec = new TableSpec(table.Id);
            var tb = await unitOfWork.Repository<Table>().GetByIdWithSpecAsync(Spec);

            if (tb == null)
            {
                throw new InvalidOperationException("Table not found.");
            }

            if (tb.IsAvailable == TableStatus.NotAvalible)
            {
                throw new InvalidOperationException("The table is already reserved.");
            }

            // Mark the table as not available
            table.IsAvailable = TableStatus.NotAvalible;

            // Update the table
            var tableRepo = unitOfWork.Repository<Table>();
            tableRepo.Update(tb);

            // Commit the changes for table first
            await unitOfWork.Complete();

            // Create the reservation with the provided reservation name
            var reservation = new Reservison
            {
                TableId = table.Id,
                ReservationName = reservationName,  // Set the ReservationName here
                ReservationDate = DateTime.Now,
                ReservationEndTime = DateTime.UtcNow.AddHours(23), // Example end time
                IsCompleted = false
            };

            // Add the reservation
            var reservationRepo = unitOfWork.Repository<Reservison>();
            await reservationRepo.AddAsync(reservation);

            // Commit the transaction for reservation
            await unitOfWork.Complete();

            return reservation;
        }
        public Task<Table> GetTableById(int id)
        {
            var Spec = new TableSpec(id);
            var tb = unitOfWork.Repository<Table>().GetByIdWithSpecAsync(Spec);
            return tb;
            
        }
    }
}

