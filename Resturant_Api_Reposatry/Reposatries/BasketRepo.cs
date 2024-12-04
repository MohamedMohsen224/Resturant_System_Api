using Resturant_Api_Core.Entites.Basket;
using Resturant_Api_Core.Reposatries;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.Reposatries
{
    public class BasketRepo : IBasketReposatry
    {
        private readonly IDatabase _database;
        public BasketRepo(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
           return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket?>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var CreatedBasket = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if(CreatedBasket is false) 
            {
                return null;
            }
            return await GetBasketAsync(basket.Id);
        }
    }
}
