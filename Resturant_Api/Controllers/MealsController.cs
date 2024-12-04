using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant_Api.Dtos;
using Resturant_Api.HandleErrors;
using Resturant_Api.Helper;
using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.MealsSpecifcation;

namespace Resturant_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMealsServices mealsServices;
        private readonly IMapper mapper;

        public MealsController(IMealsServices mealsServices ,IMapper mapper)
        {
            this.mealsServices = mealsServices;
            this.mapper = mapper;
        }
        [HttpGet("GetAllMealsInMenu")]
        public async Task<ActionResult<Pagination<MealDto>>> GetAllMeals([FromQuery] MealsParms parms)
        {
            var meals = await mealsServices.GetAllMeals(parms);
            var Meal = mapper.Map<IReadOnlyList<Meals>, IReadOnlyList<MealDto>>(meals);
            if (Meal == null)
            {
                return NotFound(new ApiErrorResponse(404));
            }
            return Ok(Meal);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Meals>> GetMealsById(int Id)
        {
            var meal = await mealsServices.GetMealsById(Id);
            var Meal = mapper.Map<Meals, MealDto>(meal);
            if (Meal == null)
            {
                return NotFound(new ApiErrorResponse(404));
            }
            return Ok(Meal);
        }

        [HttpPost]
        public async Task<ActionResult<MealDto>> CreateMeals(MealDto mealDto)
        {
            var meal = mapper.Map<MealDto, Meals>(mealDto);
            var Meal = await mealsServices.CreateMeals(meal);
            var MealDto = mapper.Map<Meals, MealDto>(Meal);
            return Ok(MealDto);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<MealDto>> UpdateMeals(MealDto mealDto, int Id)
        {
            var meal = mapper.Map<MealDto, Meals>(mealDto);
            var Currentmeal = await mealsServices.GetMealsById(Id);
            if (Currentmeal == null)
            {
                return NotFound(new ApiErrorResponse(404));
            }
            mealsServices.UpdateMeals(meal, Id);
            return Ok(mealDto);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteMeals(int Id)
        {
            var meal = await mealsServices.GetMealsById(Id);
            if (meal == null)
            {
                return NotFound(new ApiErrorResponse(404));
            }
            mealsServices.DeleteMeals(Id);
            return Ok();
        }


    }
}
