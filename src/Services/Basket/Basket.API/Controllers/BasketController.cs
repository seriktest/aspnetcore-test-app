using Basket.API.GrpcServices;

namespace Basket.API.Controllers
{
    using System.Net;

    using Entities;
    using Repositories;

    using Microsoft.AspNetCore.Mvc;

   
    [ApiController]
    [Route("api/v1/[controller]/{userName}")]
    internal class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly  DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        [HttpGet("username", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasketAsync(userName).ConfigureAwait(false);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            foreach (var item in basket.Items) {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            
            return Ok(await _repository.UpdateBasketAsync(basket).ConfigureAwait(false));
        }

        [HttpDelete("", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public Task DeleteBasket(string userName)
        {
            return _repository.DeleteBasketAsync(userName);
        }
    }
}