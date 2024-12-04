using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Specification.TableSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Services.EntitesServices
{
    public interface ITableServices
    {
        Task<IReadOnlyList<Table>> GetAllTables(TableParms parms);
        Task<Table> GetTableById(int id);
        Task<IReadOnlyList<Table>> GetAvalibleTables(TableParms parms);
        Task<Table> CreateTable(Table table);
        Task<Reservison> ReserveTable(Table table);

    }
}
