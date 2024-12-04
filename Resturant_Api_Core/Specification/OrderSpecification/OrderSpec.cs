using Resturant_Api_Core.Entites.Order_Mangment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Specification.OrderSpecification
{
    public class OrderSpec : BaseSpecification<Order>
    {
        public OrderSpec(string buyerEmail)
           : base(O => O.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.orderItem);

            AddOrderByDesc(o => o.OrderDate);
        }

        public OrderSpec(int orderId, string buyerEmail)
            : base(o => o.Id == orderId && o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.orderItem);
        }
    }
}
