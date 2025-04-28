using EShopping.Discount.Data;
using EShopping.Discount.Data.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EShopping.Discount.Grpc.Services
{
    public class DiscountService(DiscountContext db, ILogger<DiscountService> logger) : Discount.DiscountBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await db.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName) ?? Coupon.Empty;

            logger.LogInformation("Discount is retrieved for ProductName: {ProductName} with Ammount: {Ammount}", coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Coupon cannot be null"));

            var res = await db.Coupons.AddAsync(coupon);
            await db.SaveChangesAsync();

            if (res?.Entity is null)
                throw new RpcException(new Status(StatusCode.Internal, "Error creating coupon"));
            
            logger.LogInformation("Discount is successfully created for ProductName: {ProductName} with Ammount: {Ammount}", coupon.ProductName, coupon.Amount);

            return res.Entity.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var newcoupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Coupon cannot be null"));
            var coupon = await db.Coupons.FindAsync(request.Coupon.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));

            db.Entry(coupon).CurrentValues.SetValues(newcoupon);
            await db.SaveChangesAsync();

            logger.LogInformation("Discount is successfully updated for ProductName: {ProductName} with Ammount: {Ammount}", coupon.ProductName, coupon.Amount);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await db.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                return new DeleteDiscountResponse() { Success = false };

            db.Coupons.Remove(coupon);
            await db.SaveChangesAsync();

            logger.LogInformation("Discount for product {ProductName} deleted successfully", request.ProductName);

            return new DeleteDiscountResponse() { Success = true };
        }
    }
}
