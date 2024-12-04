namespace Resturant_Api.Dtos.Order_Basket
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string MealsName { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
    }
}
