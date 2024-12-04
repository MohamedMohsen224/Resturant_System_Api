using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant_Api.Dtos;
using Resturant_Api.HandleErrors;
using Resturant_Api.Helper;
using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.CategorySpecification;

namespace Resturant_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;
        private readonly IMapper mapper;

        public CategoryController(ICategoryServices categoryServices , IMapper mapper)
        {
            this.categoryServices = categoryServices;
            this.mapper = mapper;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            var category = mapper.Map<CategoryDto, Category>(categoryDto);
            var Category = await categoryServices.CreateCategory(category);
            var CategoryDto = mapper.Map<Category, CategoryDto>(Category);
            return Ok(CategoryDto);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteCategory(int Id)
        {
            var isdeleted = await categoryServices.DeleteCategory(Id);
            if (isdeleted)
            {
                return Ok("Category is deleted");
            }
            return NotFound(new ApiErrorResponse(404, "No category with this name"));
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<Pagination<CategoryDto>>> GetAllCategories([FromQuery] CategoryParms parms)
        {
            var cats = await categoryServices.GetAllCategories(parms);
            var Cats = mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDto>>(cats);
            if (Cats == null)
            {
                return NotFound(new ApiErrorResponse(404 ,"No Categories Wait For Updates"));
            }
            return Ok(Cats);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int Id)
        {
            var cat = await categoryServices.GetCategoryById(Id);
            var Cat = mapper.Map<Category, CategoryDto>(cat);
            if (Cat == null)
            {
                return NotFound(new ApiErrorResponse(404 ,"No Category"));
            }
            return Ok(Cat);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody]Category category, int Id)
        {
            var cat = await categoryServices.GetCategoryById(Id);
            if (cat == null || Id != cat.Id)
            {
                return BadRequest(new ApiErrorResponse(400, "Invalid category data or mismatched ID."));
            }

            try
            {
                // Call the service to update the category
                var isUpdated = await categoryServices.UpdateCategory(cat, Id);

                if (!isUpdated)
                {
                    return NotFound(new ApiErrorResponse(404, "Category not found."));
                }

                // Return success response
                return Ok(new { message = "Category updated successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "Error occurred while updating category.");

                return StatusCode(500, new ApiErrorResponse(500, $"Internal server error: {ex.Message}"));
            }

        }
    }
}
