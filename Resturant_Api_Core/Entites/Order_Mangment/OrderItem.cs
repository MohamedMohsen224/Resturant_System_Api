namespace Resturant_Api_Core.Entites.Order_Mangment
{
    public class OrderItem :Base
    {
        public OrderItem()
        {
            
        }
        public OrderItem(MealsInOrder meals , int quantity , decimal price)
        {
            this.meals = meals;
            this.Quantity = quantity;
            this.Price = price; 
        }


        public MealsInOrder meals { get; set; } 
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    
    }
}