using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Reposatries;
using Resturant_Api_Core.Specification.MealsSpecifcation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Services.EntitesServices
{
    public interface IMealsServices
    {
        public Task<IReadOnlyList<Meals>> GetAllMeals(MealsParms parms);
        public Task<Meals> GetMealsById(int Id);
        public Task<Meals> CreateMeals(Meals meals);
        public Task<bool> UpdateMeals(Meals meals , int  Id);
        public Task<bool> DeleteMeals(int Id);
    }
}
