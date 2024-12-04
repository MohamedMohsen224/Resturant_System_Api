using Resturant_Api_Core.Reposatries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Specification
{
    public class BaseSpecification<T>: ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Cretaria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; }

        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<T, bool>> cretaria)
        {
            this.Cretaria = cretaria;
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDesc)
        {
            this.OrderByDesc = OrderByDesc;
        }
        public void ApplayPagination(int skip, int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;

        }
    }
}
