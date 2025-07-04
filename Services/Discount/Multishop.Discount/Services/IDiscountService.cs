using Multishop.Discount.Dtos;

namespace Multishop.Discount.Services
{
    public interface IDiscountService
    {
        Task<List<ResultDiscountCouponDto>> GetAllCouponsAsync();
        Task CreateCouponAsync(CreateDiscountCouponDto createCouponDto);
        Task UpdateCouponAsync(UpdateDiscountCouponDto updateCouponDto);
        Task DeleteCouponAsync(int couponId);
        Task<GetByIdDiscountCouponDto> GetByIdCouponAsync(int couponId);
    }
}
