using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Resturant_Api_Core.Entites.Enum;
using Resturant_Api_Core.Entites.User;

namespace Resturant_Api_Core.Entites.Order_Mangment
{
    public class Order : Base
    {
        public Order()
        {
            
        }

        public Order(ICollection<OrderItem> orderItem, string buyerEmail, Address address, DeliveryMethod deliveryMethod, decimal subTotal)
        {
            this.orderItem = orderItem;
            BuyerEmail = buyerEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal SubTotal { get; set; }
        public Status status { get; set; } = Status.pending;
        public ICollection<OrderItem> orderItem { get; set; } = new HashSet<OrderItem>();
        public Address Address { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string PaymentIntentId { get; set; } = "";
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
    }
}
