using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace Api.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetById(string id)
        {
            var b = await basketRepository.GetBasketAsync(id);

            return Ok(b ?? new Basket(id));
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket(Basket basket)
        {
            var updated = await basketRepository.UpdateBasketAsync(basket);
            return Ok(updated);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await basketRepository.DeleteBasketAsync(id);
        }
    }
}
