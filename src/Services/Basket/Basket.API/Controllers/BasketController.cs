namespace Basket.API.Controllers
{
    using System.Net;

    using Basket.API.Entities;
    using Basket.API.Repositories;

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
            this._repository = repository;
        }

        [HttpGet("", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await this._repository.GetBasketAsync(userName).ConfigureAwait(false);
            return this.Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            var updatedBasket = await this._repository.UpdateBasketAsync(basket).ConfigureAwait(false);
            return this.Ok(updatedBasket);
        }

        [HttpDelete("", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasket(string userName)
        {
            await this._repository.DeleteBasketAsync(userName).ConfigureAwait(false);
        }
    }
}