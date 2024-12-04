using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant_Api_Core.Entites
{
    public class Meals : Base
    {
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [MaxLength(60), MinLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200), MinLength(20)]
        public string Components { get; set; }
        [Required]
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}