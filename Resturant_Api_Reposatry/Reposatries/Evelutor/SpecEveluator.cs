using Microsoft.EntityFrameworkCore;
using Resturant_Api_Core.Reposatries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.Reposatries.Evelutor
{
    public class SpecEveluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if (inputQuery == null)
            {
                throw new ArgumentNullException(nameof(inputQuery), "The input query cannot be null.");
            }

            if (spec == null)
            {
                throw new ArgumentNullException(nameof(spec), "The specification cannot be null.");
            }

            var query = inputQuery;

            // Apply criteria filter if specified
            if (spec.Cretaria is not null)
            {
                query = query.Where(spec.Cretaria);
            }

            // Apply ordering if specified
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            // Apply pagination if enabled
            if (spec.IsPaginationEnable)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // Apply include expressions (if any exist)
            if (spec.Includes != null && spec.Includes.Any())
            {
                query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) =>
                {
                    if (includeExpression == null)
                    {
                        throw new ArgumentNullException(nameof(includeExpression), "Include expression cannot be null.");
                    }
                    return currentQuery.Include(includeExpression);
                });
            }

            return query;
        }

    }
}
