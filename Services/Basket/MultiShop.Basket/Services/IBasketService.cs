using MultiShop.Basket.Dtos;

namespace MultiShop.Basket.Services
{
    public interface IBasketService
    {
        Task<BasketTotalDTO> GetBasket(string userid);
        Task SaveBasket(BasketTotalDTO basketTotalDTO);
        Task DeleteBasket(string userid);
    }
}
