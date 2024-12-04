﻿using System.Linq.Expressions;

namespace Resturant_Api_Core.Reposatries
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Cretaria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; }


        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDesc { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IsPaginationEnable { get; set; }
    }
}