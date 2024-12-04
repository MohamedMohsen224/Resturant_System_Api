using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Entites.Order_Mangment;
using Resturant_Api_Core.Entites.User;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Core.Reposatries;
using Resturant_Api_Core.Services.OrderServises;
using Resturant_Api_Core.Specification.MealsSpecifcation;
using Resturant_Api_Core.Specification.OrderSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Services.EntitesSERVICES.OrderServ
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketReposatry reposatry;

        public OrderServices(IUnitOfWork unitOfWork , IBasketReposatry reposatry)
        {
            this.unitOfWork = unitOfWork;
            this.reposatry = reposatry;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var Basket = await reposatry.GetBasketAsync(basketId);

            var OrderMeals = new List<OrderItem>();
            if(Basket?.Items?.Count > 0)
            {
                var mealsRepo =  unitOfWork.Repository<Meals>();
                foreach(var item in Basket.Items)
                {
                    var Spec = new MealsSpecific(item.Id);
                    var Meal = await mealsRepo.GetByIdWithSpecAsync(Spec);

                    var MealItemOrder = new MealsInOrder(item.Id, Meal.Name, Meal.PictureUrl);
                    var OrderItem = new OrderItem(MealItemOrder, item.Quantity, Meal.Price);
                    OrderMeals.Add(OrderItem);
                }
            }

            var TotalPrice = OrderMeals.Sum(M=>M.Price*M.Quantity);

            var DeliveryWay = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var Order = new Order(OrderMeals,buyerEmail, shippingAddress, DeliveryWay,TotalPrice);

            var Result = await unitOfWork.Complete();
            if(Result <= 0)
            {
                return null;
            }
           return Order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
            => await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        

        public Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var OrderRepo = unitOfWork.Repository<Order>();
            var OrderSpe = new OrderSpec(orderId, buyerEmail);
            var ORDER = OrderRepo.GetByIdWithSpecAsync(OrderSpe);
            return ORDER;
           
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var OrderRepo = unitOfWork.Repository<Order>();
            var OrderSpe = new OrderSpec(buyerEmail);
            var ORDERs = OrderRepo.GetAllWithSpecAsync(OrderSpe);
            return ORDERs;
        }
    }
}
