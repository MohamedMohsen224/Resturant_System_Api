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
        public Task<Reservison> ReserveTable(Table table)
        {
            if (table.IsAvailable == TableStatus.NotAvalible)
            {
                throw new Exception("Table is already reserved");
            }
            else
            {
                var res = new Reservison
                {
                    TableId = table.Id,
                    Table = table,
                    ReservationDate = DateTime.Now,
                    ReservationEndTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(23))
                };
              
                table.IsAvailable = TableStatus.Avalible;
                unitOfWork.Repository<Table>().Update(table);
                unitOfWork.Repository<Reservison>().AddAsync(res);
                if (res.ReservationEndTime == TimeOnly.FromDateTime(DateTime.Now))
                    unitOfWork.Repository<Reservison>().Delete(res);
                unitOfWork.Complete();
                return Task.FromResult(res);
            };
                

        }
    }
}

