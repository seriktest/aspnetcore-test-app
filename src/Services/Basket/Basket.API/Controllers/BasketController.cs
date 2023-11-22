using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers; 

[ApiController]
[Route("api/v1/[controller]/{userName}")]
public class BasketController : ControllerBase {
    private readonly IBasketRepository _repository;
    
    public BasketController(IBasketRepository repository) {
        _repository = repository;
    }
    
    [HttpGet("", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public  async Task<ActionResult<ShoppingCart>> GetBasket(string userName) {
        var basket = await _repository.GetBasket(userName);
        return Ok(basket ?? new ShoppingCart(userName));
    }
        
    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket) {
        var updatedBasket = await _repository.UpdateBasket(basket);
        return Ok(updatedBasket);
    }
    
    [HttpDelete("", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task DeleteBasket(string userName) {
        await _repository.DeleteBasket(userName);
    }

}