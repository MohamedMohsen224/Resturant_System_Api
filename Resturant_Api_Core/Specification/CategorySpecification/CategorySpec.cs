using Resturant_Api_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Specification.CategorySpecification
{
    public class CategorySpec : BaseSpecification<Category>
    {
        public CategorySpec(CategoryParms parms): base(x =>
                   (string.IsNullOrEmpty(parms.Search) || x.Name.ToLower().Contains(parms.Search)) &&
                   (!parms.CategoryId.HasValue || x.Id == parms.CategoryId)
               )
        {
            ApplayPagination(parms.PageSize * (parms.PageIndex - 1), parms.PageSize);
            if (!string.IsNullOrEmpty(parms.Sort))
            {
                switch (parms.Sort)
                {
                    case "id":
                        AddOrderBy(x => x.Id);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public CategorySpec(int Id) : base(x => x.Id == Id)
        {

        }
    }
}
