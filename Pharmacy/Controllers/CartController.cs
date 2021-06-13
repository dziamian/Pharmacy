using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Controllers.BaseControllers;
using Pharmacy.Models.Data_Transfrom_Objects.Cart;
using Pharmacy.Services;

namespace Pharmacy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : AuthControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemReadDto>>> GetCart()
        {
            string uid = GetUID();
            var cart = await _cartService.GetCart(uid);
            
            return Ok(cart);
        }
        
        [HttpGet("{id}", Name = nameof(GetCartItem))]
        public async Task<ActionResult<CartItemReadDto>> GetCartItem(int id)
        {
            string uid = GetUID();
            var cartItem = await _cartService.GetCartItem(uid, id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return Ok(cartItem);
        }

        [HttpGet("validate")]
        public async Task<ActionResult> ValidateCart()
        {
            string uid = GetUID();
            var status = await _cartService.ValidateCart(uid);

            if (!status) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("size")]
        public async Task<ActionResult<int>> GetNumberOfItemsInCart()
        {
            string uid = GetUID();
            var cart = await _cartService.GetCart(uid);

            return Ok(cart.Count());
        }

        [HttpPut]
        public async Task<ActionResult> AddItemToCart(CartItemCreateDto cartItemCreateDto)
        {
            if (cartItemCreateDto.Amount <= 0)
            {
                return BadRequest();
            }

            string uid = GetUID();
            var result = await _cartService.AddItemToCart(uid, cartItemCreateDto.ProductId, cartItemCreateDto.Amount);
            
            if (!result)
            {
                return NotFound("Product not found.");
            }

            return Ok("Successfully added to cart.");
        }

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> RemoveItemFromCart(int id)
        {
            string uid = GetUID();
            var result = await _cartService.RemoveItemFromCart(uid, id);
            
            if (result)
            {
                return Ok("Successfully removed item from cart.");
            }
            return NotFound("Product not found.");
        }

        [HttpDelete("remove")]
        public async Task<ActionResult> RemoveCart()
        {
            string uid = GetUID();
            await _cartService.RemoveItemsFromCart(uid);

            return Ok();
        }
    }
}
