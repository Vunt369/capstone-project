﻿using _2Sport_BE.Helpers;
using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Services;
using _2Sport_BE.Services.Caching;
using _2Sport_BE.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2Sport_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartWithRedisController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartItemService _cartItemService;
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IMapper _mapper;

        public CartWithRedisController(IUnitOfWork unitOfWork,
                                       ICartItemService cartItemService,
                                       IWarehouseService warehouse,
                                       IProductService productService,
                                       IRedisCacheService redisCacheService,
                                       IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cartItemService = cartItemService;
            _productService = productService;
            _redisCacheService = redisCacheService;
            _warehouseService = warehouse;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-cart")]
        public async Task<IActionResult> GetCarts([FromQuery] DefaultSearch defaultSearch)
        {
            try
            {
                var userId = GetCurrentUserIdFromToken();

                if (userId == 0)
                {
                    return Unauthorized();
                }
                var query = _redisCacheService.GetData<List<CartItem>>("CartItems");
                //var query = await _cartItemService.GetCartItems(userId, defaultSearch.currentPage, defaultSearch.perPage);
                if (query != null)
                {
                    var cartItems = query.Select(_ => _mapper.Map<CartItem, CartItemVM>(_)).ToList();
                    if (cartItems != null)
                    {
                        foreach (var carItem in cartItems)
                        {
                            var product = await _unitOfWork.ProductRepository.FindAsync(carItem.ProductId);
                            carItem.ProductName = product.ProductName;
                            carItem.ProductCode = product.ProductCode;
                            carItem.Color = product.Color;
                            carItem.Size = product.Size;
                            carItem.Price = (decimal)product.Price;
                            carItem.Condition = (int)product.Condition;
                            carItem.ImgAvatarPath = product.ImgAvatarPath;
                            carItem.RentPrice = (decimal)product.RentPrice;
                        }
                        return Ok(new { total = cartItems.Count(), data = cartItems });
                    }
                    return BadRequest();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet]
        [Route("get-cart-item/{cartItemId}")]
        public async Task<IActionResult> GetCartItem(Guid cartItemId)
        {
            try
            {
                var cartItem = await _cartItemService.GetCartItemById(cartItemId);
                if (cartItem != null)
                {
                    return Ok(cartItem);
                }
                return BadRequest($"Cannot find cart item with id: {cartItemId}");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("add-to-cart")]
        public async Task<IActionResult> AddToCart(CartItemCM cartItemCM)
        {
            try
            {
                var userId = GetCurrentUserIdFromToken();

                if (userId == 0)
                {
                    return Unauthorized();
                }

                var newCartItem = _mapper.Map<CartItemCM, CartItem>(cartItemCM);
                var productInWarehouse = (await _warehouseService.GetWarehouseByProductId(cartItemCM.ProductId))
                                        .FirstOrDefault();
                var addedProduct = (await _productService.GetProductById((int)cartItemCM.ProductId));
                if (productInWarehouse == null)
                {
                    return BadRequest("That product is not in warehouse!");
                }
                var quantityOfProduct = productInWarehouse.AvailableQuantity;
                var addedCartItemQuantity = cartItemCM.Quantity;
                var listCartItemsInCache = _redisCacheService.GetData<List<CartItem>>("CartItems") 
                                                                        ?? new List<CartItem>();
                var existedCartItem = listCartItemsInCache.FirstOrDefault(_ => _.UserId == userId
                                                                && _.ProductId == (int)cartItemCM.ProductId);
                //var existedCartItem = await _cartItemService.GetCartItemByUserIdAndProductId(userId, (int)cartItemCM.ProductId);
                var addedCartItem = new CartItem();
                if (existedCartItem != null)
                {
                    addedCartItemQuantity += existedCartItem.Quantity;
                    if (addedCartItemQuantity > quantityOfProduct)
                    {
                        return BadRequest($"Xin lỗi! Chúng tôi chỉ còn {quantityOfProduct} sản phẩm");
                    }
                    existedCartItem.Quantity = addedCartItemQuantity;
                    existedCartItem.Price = addedProduct.Price * addedCartItemQuantity;
                    _redisCacheService.SetData("CartItems", listCartItemsInCache);
                    return Ok(existedCartItem);
                    //addedCartItem = await _cartItemService.AddExistedCartItem(existedCartItem);

                }
                else
                {
                    if (addedCartItemQuantity > quantityOfProduct)
                    {
                        return BadRequest($"Xin lỗi! Chúng tôi chỉ còn {quantityOfProduct} sản phẩm");
                    }
                    newCartItem.CartItemId = Guid.NewGuid();
                    newCartItem.UserId = userId;
                    listCartItemsInCache.Add(newCartItem);
                    _redisCacheService.SetData("CartItems", listCartItemsInCache);
                    return Ok(newCartItem);
                    //addedCartItem = await _cartItemService.AddNewCartItem(userId, newCartItem);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("reduce-cart/{cartItemId}")]
        public async Task<IActionResult> ReduceCart(int cartItemId)
        {
            try
            {
                var userId = GetCurrentUserIdFromToken();

                if (userId == 0)
                {
                    return Unauthorized();
                }

                await _cartItemService.ReduceCartItem(cartItemId);
                return Ok($"Reduce cart item with id: {cartItemId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("update-quantity-cart-item/{cartItemId}")]
        public async Task<IActionResult> UpdateQuantityOfCart(Guid cartItemId, [FromQuery] int quantity)
        {
            try
            {
                var userId = GetCurrentUserIdFromToken();

                if (userId == 0)
                {
                    return Unauthorized();
                }
                var cartItem = await _cartItemService.GetCartItemById(cartItemId);
                var quantityOfProduct = (await _warehouseService.GetWarehouseByProductId(cartItem.ProductId))
                        .FirstOrDefault().AvailableQuantity;
                if (quantity > quantityOfProduct)
                {
                    return BadRequest($"Xin lỗi! Chúng tôi chỉ còn {quantityOfProduct} sản phẩm");
                }
                await _cartItemService.UpdateQuantityOfCartItem(cartItemId, quantity);
                return Ok($"Update quantity cart item with id: {cartItemId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("delete-cart-item/{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(Guid cartItemId)
        {
            try
            {
                var userId = GetCurrentUserIdFromToken();

                if (userId == 0)
                {
                    return Unauthorized();
                }

                await _cartItemService.DeleteCartItem(cartItemId);
                return Ok($"Delete cart item with id: {cartItemId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        protected int GetCurrentUserIdFromToken()
        {
            int UserId = 0;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string strUserId = identity.FindFirst("UserId").Value;
                        int.TryParse(strUserId, out UserId);

                    }
                }
                return UserId;
            }
            catch
            {
                return UserId;
            }
        }

    }
}
