using Resturant_Api_Core.Entites.Enum;

namespace Resturant_Api.Dtos
{
    public class TableDto
    {
        public int Id { get; set; }
        public string IsAvailable { get; set; }
        public int Capacity { get; set; }
        public string Floor { get; set; }
        public string smoking { get; set; }
    }
}
