namespace Basket.API.Controllers
{
    using System.Net;

    using Entities;
    using Repositories;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The basket controller.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]/{userName}")]
    internal class BasketController : ControllerBase
    {
        /// <summary>
        /// The _repository.
        /// </summary>
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("", Name = "GetBasket")]
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
            var updatedBasket = await _repository.UpdateBasketAsync(basket).ConfigureAwait(false);
            return Ok(updatedBasket);
        }

        [HttpDelete("", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public Task DeleteBasket(string userName)
        {
            return _repository.DeleteBasketAsync(userName);
        }
    }
}