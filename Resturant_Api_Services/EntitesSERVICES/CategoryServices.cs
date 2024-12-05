using Resturant_Api_Core.Entites;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.CategorySpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Services.EntitesSERVICES
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Category> CreateCategory(Category category)
        {
           await unitOfWork.Repository<Category>().AddAsync(category);
           await unitOfWork.Complete();
           return category;
        }

        

        public async Task<bool> DeleteCategory(int Id)
        {
            var spec = new CategorySpec(Id);
            var cat = await unitOfWork.Repository<Category>().GetByIdWithSpecAsync(spec);
            
            if (cat == null)
            {
                return false;
            }
            unitOfWork.Repository<Category>().Delete(cat);
            await unitOfWork.Complete();
            return true;

        }

        public async Task<IReadOnlyList<Category>> GetAllCategories(CategoryParms parms)
        {
            var spec = new CategorySpec(parms);
            var Cats = await unitOfWork.Repository<Category>().GetAllWithSpecAsync(spec);
            return Cats;
        }

        public async Task<Category> GetCategoryById(int Id)
        {
            var spec = new CategorySpec(Id);
            var Cat = await unitOfWork.Repository<Category>().GetByIdWithSpecAsync(spec);
            return Cat;
        }

        public async Task<bool> UpdateCategory(Category category, int Id)
        {
            var spec = new CategorySpec(Id);
            var cat = await unitOfWork.Repository<Category>().GetByIdWithSpecAsync(spec);
            if (cat == null)
            {
                return false;
            }
            cat.Name = category.Name;
            unitOfWork.Repository<Category>().Update(category);
            await unitOfWork.Complete();
            return true;

        }
    }
}
