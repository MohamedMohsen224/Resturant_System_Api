namespace Resturant_Api_Core.Entites.Order_Mangment
{
    public class DeliveryMethod : Base
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string name , string Desc ,decimal cost ,TimeOnly time)
        {
            this.Name = name;
            this.Description = Desc;
            this.Cost = cost;
            this.DeliveryTime = time;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public TimeOnly DeliveryTime { get; set; }
    }
}