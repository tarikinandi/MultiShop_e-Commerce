using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;

namespace MultiShop.Basket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILoginService _loginService;

        public BasketsController(ILoginService loginService , IBasketService basketservice)
        {
            _basketService = basketservice;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBasketDetail()
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı ID'si bulunamadı.");
            }
            var basket = await _basketService.GetBasket(userId);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMyBasket(BasketTotalDTO basketTotalDTO)
        {
            if (basketTotalDTO == null)
            {
                return BadRequest("Sepet verisi bulunamadı.");
            }
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı ID'si bulunamadı.");
            }
            basketTotalDTO.UserId = userId;
            await _basketService.SaveBasket(basketTotalDTO);
            return Ok("Sepetteki değişiklikler kaydedildi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMyBasket()
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı ID'si bulunamadı.");
            }
            await _basketService.DeleteBasket(userId);
            return Ok("Sepetiniz silindi.");
        }
    }
}
