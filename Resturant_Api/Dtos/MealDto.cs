using System.ComponentModel.DataAnnotations;

namespace Resturant_Api.Dtos
{
    public class MealDto
    {
        public string PictureUrl { get; set; }
        public string Name { get; set; }
        public string Components { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
    }
}
