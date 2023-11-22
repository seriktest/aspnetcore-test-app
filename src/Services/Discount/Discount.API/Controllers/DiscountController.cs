using System.Net;
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _repository.GetDiscount(productName).ConfigureAwait(false);
            return Ok(coupon).configureAwait(false);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await _repository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon).configureAwait(false);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _repository.UpdateDiscount(coupon)).configureAwait(false);
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
        {
            return Ok(await _repository.DeleteDiscount(productName)).configureAwait(false);
        }
    }
}
