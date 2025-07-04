using Dapper;
using Multishop.Discount.Context;
using Multishop.Discount.Dtos;

namespace Multishop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }


        public async Task CreateCouponAsync(CreateDiscountCouponDto createCouponDto)
        {
            string query = "insert into Coupons (Code, Rate, IsActive, ValidDate) values (@Code, @Rate, @IsActive, @ValidDate)";
            var parameters = new DynamicParameters();
            parameters.Add("Code", createCouponDto.Code);
            parameters.Add("Rate", createCouponDto.Rate);
            parameters.Add("IsActive", createCouponDto.IsActive);
            parameters.Add("ValidDate", createCouponDto.ValidDate);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCouponAsync(int couponId)
        {
            string query = "delete from Coupons where CouponId = @CouponId";
            var parameters = new DynamicParameters();
            parameters.Add("CouponId", couponId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultDiscountCouponDto>> GetAllCouponsAsync()
        {
            string query = "select * from Coupons";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdDiscountCouponDto> GetByIdCouponAsync(int couponId)
        {
            string query = "select * from Coupons where CouponId = @CouponId";
            var parameters = new DynamicParameters();
            parameters.Add("CouponId", couponId);

            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query , parameters);
                return value;
            }
        }

        public async Task UpdateCouponAsync(UpdateDiscountCouponDto updateCouponDto)
        {
            string query = "update Coupons set Code = @Code, Rate = @Rate, IsActive = @IsActive, ValidDate = @ValidDate where CouponId = @CouponId";
            var parameters = new DynamicParameters();
            parameters.Add("CouponId", updateCouponDto.CouponId);
            parameters.Add("Code", updateCouponDto.Code);
            parameters.Add("Rate", updateCouponDto.Rate);
            parameters.Add("IsActive", updateCouponDto.IsActive);
            parameters.Add("ValidDate", updateCouponDto.ValidDate);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            } 
        }
    }
}
