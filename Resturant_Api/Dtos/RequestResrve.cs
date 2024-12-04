using System.ComponentModel.DataAnnotations;

namespace Resturant_Api.Dtos
{
    public class RequestResrve
    {
        [Required]
        public int Id { get; set; }
    }
}
