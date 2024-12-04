using Resturant_Api_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Specification.MealsSpecifcation
{
    public class MealsSpecific : BaseSpecification<Meals>
    {
        public MealsSpecific(MealsParms parms):base(x=>(string.IsNullOrEmpty(parms.Search) || x.Name.ToLower().Contains(parms.Search))&&
        (!parms.MealsId.HasValue || x.Id == parms.MealsId))
        {
            ApplayPagination(parms.PageSize * (parms.PageIndex - 1), parms.PageSize);
            if (!string.IsNullOrEmpty(parms.Sort))
            {
                switch (parms.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                }
            }
        }


        public MealsSpecific(int Id):base(x=>x.Id == Id)
        {
            
        }

    }
}
