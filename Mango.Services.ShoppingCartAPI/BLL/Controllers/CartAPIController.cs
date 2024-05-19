using AutoMapper;
using Mango.Services.ShoppingCartAPI.BLL.Services.IServices;
using Mango.Services.ShoppingCartAPI.DAL.Models;
using Mango.Services.ShoppingCartAPI.DAL.Models.Dtos;
using Mango.Services.ShoppingCartAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.BLL.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartAPIController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;

        private readonly IShoppingCartRepository _cartRepo;
        private ResponseDto _response;

        public CartAPIController(IProductService productService, ICouponService couponService, IShoppingCartRepository cartRepo)
        {
            _productService = productService;
            _couponService = couponService;
            _cartRepo = cartRepo;
            _response = new();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                var cartHeader = await _cartRepo.GetCartHeaderByUserIdAsync(userId);

                var cart = new CartDto()
                {
                    CartHeader = cartHeader,
                    CartDetails = await _cartRepo.GetCartDetailsByHeaderIdAsync(cartHeader.Id)
                };

                var productDtos = await _productService.GetProductsAsync();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.Id == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCouponAsync(cart.CartHeader.CouponCode);

                    if (coupon != null && cartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }

                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDto> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeader = await _cartRepo.GetCartHeaderByUserIdAsync(cartDto.CartHeader.UserId);

                cartHeader.CouponCode = cartDto.CartHeader.CouponCode;
                await _cartRepo.UpdateCartHeaderAsync(cartHeader);

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeader = await _cartRepo.GetCartHeaderByUserIdAsync(cartDto.CartHeader.UserId);

                if (cartHeader == null)
                {
                    // Create Header and details
                    var addedCartHeader = await _cartRepo.CreateCartHeaderAsync(cartDto.CartHeader);

                    cartDto.CartDetails.First().CartHeaderId = addedCartHeader.Id;

                    await _cartRepo.CreateCartDetailsAsync(cartDto.CartDetails.First());
                }
                else
                {
                    // If header is not null
                    // Check if details has same product
                    var cartDetails = await _cartRepo.GetCartDetailsByProductAndHeaderIdAsync(cartDto.CartDetails.First().ProductId, cartHeader.Id);

                    if (cartDetails == null)
                    {
                        // Create details
                        cartDto.CartDetails.First().CartHeaderId = cartHeader.Id;
                        await _cartRepo.CreateCartDetailsAsync(cartDto.CartDetails.First());
                    }
                    else
                    {
                        // Update details
                        cartDto.CartDetails.First().Count += cartDetails.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetails.CartHeaderId;
                        cartDto.CartDetails.First().Id = cartDetails.Id;

                        await _cartRepo.UpdateCartDetailsAsync(cartDto.CartDetails.First());
                    }
                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                var cartDetails = await _cartRepo.GetCartDetailsByIdAsync(cartDetailsId);

                var totalCountOfCartItem = _cartRepo.GetTotalCountOfCartItem(cartDetails.CartHeaderId);

                await _cartRepo.DeleteCartDetailsAsync(cartDetails);
                if (totalCountOfCartItem == 1)
                {
                    var cartHeaderToRemove = await _cartRepo.GetCartHeaderByIdAsync(cartDetails.CartHeaderId);
                    await _cartRepo.DeleteCartHeaderAsync(cartHeaderToRemove);
                }

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
