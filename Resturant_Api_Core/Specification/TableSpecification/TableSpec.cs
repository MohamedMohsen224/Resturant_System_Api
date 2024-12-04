using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Entites.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Specification.TableSpecification
{
    public class TableSpec : BaseSpecification<Table>
    {
        public TableSpec(TableParms parms):base(x=>
        (string.IsNullOrEmpty(parms.Search) || x.Floor.ToLower().Contains(parms.Search)) &&
        (!parms.TableCapacity.HasValue || x.Capacity == parms.TableCapacity)
        )
        {
            Includes.Add(x => x.Reservation);
            ApplayPagination(parms.PageSize * (parms.PageIndex - 1), parms.PageSize);
            if (!string.IsNullOrEmpty(parms.Sort))
            {
                switch (parms.Sort)
                {
                    case "capacity":
                        AddOrderBy(x => x.Capacity);
                        break;
                    case "floor":
                        AddOrderBy(x => x.Floor);
                        break;
                    case "IsAvalible":
                        AddOrderBy(x => x.IsAvailable == TableStatus.Avalible);
                        break;
                    default:
                        AddOrderBy(x => x.Floor);
                        break;
                }
            }
        }
        public TableSpec(int cap) : base(x => x.Capacity == cap)
        {
            
        }
        
    }
}
