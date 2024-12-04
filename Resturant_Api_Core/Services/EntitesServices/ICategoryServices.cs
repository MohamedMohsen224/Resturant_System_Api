using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Specification.CategorySpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Services.EntitesServices
{
    public interface ICategoryServices
    {
        Task<IReadOnlyList<Category>> GetAllCategories(CategoryParms parms);
        Task<Category> GetCategoryById(int Id);
        Task<Category> CreateCategory(Category category);
        Task <bool> UpdateCategory(Category category, int Id);
        Task <bool> DeleteCategory(int Id);
    }
}
