using Resturant_Api_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Specification.ReservisonSpecification
{
    public class ReserveSpec : BaseSpecification<Reservison>
    {
        public ReserveSpec(ResrveParms parms):base(x=> (string.IsNullOrEmpty(parms.Search) || x.ReservationName.ToLower().Contains(parms.Search)) &&
        (!parms.ReserveId.HasValue || x.Id == parms.ReserveId))
        {
            ApplayPagination(parms.PageSize * (parms.PageIndex - 1), parms.PageSize);
            if (!string.IsNullOrEmpty(parms.Sort))
            {
                switch(parms.Sort)
                {
                    case "Date":
                        AddOrderBy(x => x.ReservationDate);
                        break;
                    default:
                        AddOrderBy(x => x.ReservationName);
                        break;     
                }
            }
            
        }

        public ReserveSpec(int Id) :base(x=>x.Id == Id)
        {
            
        }
       
    }
}
