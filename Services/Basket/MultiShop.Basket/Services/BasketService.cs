using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using System.Text.Json;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task DeleteBasket(string userid)
        {
            await _redisService.GetDb().KeyDeleteAsync(userid);
        }

        public async Task<BasketTotalDTO> GetBasket(string userid)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userid);
            return JsonSerializer.Deserialize<BasketTotalDTO>(existBasket);
        }

        public async Task SaveBasket(BasketTotalDTO basketTotalDTO)
        {
            await _redisService.GetDb().StringSetAsync(basketTotalDTO.UserId, 
                JsonSerializer.Serialize(basketTotalDTO));
        }
    }
}
