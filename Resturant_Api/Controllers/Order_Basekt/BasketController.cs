using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant_Api.Dtos.Order_Basket;
using Resturant_Api.HandleErrors;
using Resturant_Api_Core.Entites.Basket;
using Resturant_Api_Core.Reposatries;

namespace Resturant_Api.Controllers.Order_Basekt
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketReposatry basketReposatry;
        private readonly IMapper mapper;

        public BasketController(IBasketReposatry basketReposatry,IMapper mapper)
        {
            this.basketReposatry = basketReposatry;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string Id)
        {
            var basket = await basketReposatry.GetBasketAsync(Id);
            return Ok(basket ?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrUpdatedBasket = await basketReposatry.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdatedBasket is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(createdOrUpdatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string Id)
        {
            await basketReposatry.DeleteBasketAsync(Id);
        }
    }
}
