namespace Resturant_Api.Dtos.Order_Basket
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>(); 
    }
}
