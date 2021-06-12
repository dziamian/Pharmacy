using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class CartService
    {
        private readonly IProductsRepo _productsRepo;
        private readonly ICartRepo _cartRepo;
        public CartService(IProductsRepo productsRepo, ICartRepo cartRepo)
        {
            _productsRepo = productsRepo;
            _cartRepo = cartRepo;
        }

        public async Task<bool> AddItemToCart(string uid, int productId, int amount = 1)
        {
            var item = await _cartRepo.GetByClientAndProductId(uid, productId, true);
            if (item == null)
            {
                var product = await _productsRepo.GetProductById(productId);
                if (product == null)
                {
                    return false;
                }
                if (product.Supply >= amount)
                {
                    await _cartRepo.CreateItem(new CartItem { ClientId = uid, ProductId = productId, Amount = amount });
                    await _cartRepo.SaveChanges();
                    return true;
                }
                return false;
            }

            if (item.Product.Supply >= item.Amount + amount)
            {
                item.Amount += amount;
                _cartRepo.UpdateItem(item);
                await _cartRepo.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CartItemDTO>> GetCart(string uid)
        {
            var cartItems = await _cartRepo.GetByClientId(uid, true);
            
            var cart = CartConverter.ToCartItemDTOs(cartItems.ToList());

            return cart;
        }

        public async Task<bool> ValidateCart(string uid)
        {

            var cartItems = await _cartRepo.GetByClientId(uid, true);

            if (cartItems.Count() == 0)
            {
                return false;
            }

            foreach (var cartItem in cartItems)
            {
                if (cartItem.Amount > cartItem.Product.Supply)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> RemoveItemFromCart(string uid, int productId)
        {
            var result = await _cartRepo.RemoveClientItem(uid, productId);
            if (result)
            {
                await _cartRepo.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task RemoveItemsFromCart(string uid)
        {
            await _cartRepo.RemoveClientItems(uid);
            await _cartRepo.SaveChanges();
        }
    }
}
