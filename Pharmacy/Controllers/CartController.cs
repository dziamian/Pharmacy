using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Controllers.BaseControllers;
using Pharmacy.Models.Data_Transfrom_Objects;
using Pharmacy.Models.Database;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories;
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

        [HttpPost]
        public async Task<ActionResult> AddItemToCart(int id, int amount = 1)
        {
            string uid = GetUID();
            var result = await _cartService.AddItemToCart(uid, id, amount);
            
            if (result)
            {
                return Ok("Successfully added to cart.");
            }
            return BadRequest("Product not found.");
        }

        [HttpDelete("remove")]
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

        [HttpGet("remove")]
        public async Task<ActionResult> RemoveCart()
        {
            string uid = GetUID();
            await _cartService.RemoveItemsFromCart(uid);

            return Ok();
        }
    }
}
