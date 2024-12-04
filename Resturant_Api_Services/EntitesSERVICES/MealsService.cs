using Resturant_Api_Core.Entites;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.MealsSpecifcation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Services.EntitesSERVICES
{
    public class MealsService : IMealsServices
    {
        private readonly IUnitOfWork unitOfWork;

        public MealsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Meals> CreateMeals(Meals meals)
        {
            var meal = await unitOfWork.Repository<Meals>().AddAsync(meals);
            await unitOfWork.Complete();
            return meal;
        }

        public async Task<bool> DeleteMeals(int Id)
        {
            var Spec = new MealsSpecific(Id);
            var ExsistMeal = await unitOfWork.Repository<Meals>().GetByIdWithSpecAsync(Spec);
            if(ExsistMeal == null)
            { return false; }
            unitOfWork.Repository<Meals>().Delete(ExsistMeal);  
            await unitOfWork.Complete();
            return true;
        }

        public async Task<IReadOnlyList<Meals>> GetAllMeals(MealsParms parms)
        {
            var Spec = new MealsSpecific(parms);
            var Meals = await unitOfWork.Repository<Meals>().GetAllWithSpecAsync(Spec);
            if (Meals == null)
            { throw new NullReferenceException();}
            return Meals;
        }

        public async Task<Meals> GetMealsById(int Id)
        {
            var Spec = new MealsSpecific(Id);
            var Meal = await unitOfWork.Repository<Meals>().GetByIdWithSpecAsync(Spec);
            if(Meal == null)
            { throw new NullReferenceException();}
            return Meal;
        }

        public async Task<bool> UpdateMeals(Meals meals, int Id)
        {
            var Spec = new MealsSpecific(Id);
            var Meal = await unitOfWork.Repository<Meals>().GetByIdWithSpecAsync(Spec);
            if (Meal == null) { return false; }
            Meal = meals;
            unitOfWork.Repository<Meals>().Update(Meal);
            await unitOfWork.Complete();
            return true;
           
        }
    }
}
