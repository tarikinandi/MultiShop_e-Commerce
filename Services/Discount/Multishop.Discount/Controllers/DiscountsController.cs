using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multishop.Discount.Dtos;
using Multishop.Discount.Services;

namespace Multishop.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountCouponList()
        {
            var values = await _discountService.GetAllCouponsAsync();
            return Ok(values);
        }

        [HttpGet("{couponId}")]
        public async Task<IActionResult> GetDiscountCouponById(int couponId)
        {
            var value = await _discountService.GetByIdCouponAsync(couponId);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
        {
            await _discountService.CreateCouponAsync(createCouponDto);
            return Ok("Kupon Başarıyla Oluşturuldu.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiscountCoupon(int couponId)
        {
            await _discountService.DeleteCouponAsync(couponId);
            return Ok("Kupon Başarıyla Silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
        {
            await _discountService.UpdateCouponAsync(updateCouponDto);
            return Ok("Kupon Başarıyla Güncellendi.");
        }
    }
}
