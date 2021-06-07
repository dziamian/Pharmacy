using Pharmacy.Models.Converters;
using Pharmacy.Models.Data_Transfrom_Objects;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class CartService : ICartService
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
            var item = await _cartRepo.GetByClientAndProductId(uid, productId, false);
            if (item == null)
            {
                var product = _productsRepo.GetProductById(productId);
                if (product == null)
                {
                    return false;
                }
                await _cartRepo.CreateItem(new CartItem { ClientId = uid, ProductId = productId, Amount = amount });
                await _cartRepo.Save();
                return true;
            }

            item.Amount += amount;
            _cartRepo.UpdateItem(item);
            await _cartRepo.Save();

            return true;
        }

        public async Task<IEnumerable<CartItemDTO>> GetCart(string uid)
        {
            var cartItems = await _cartRepo.GetByClientId(uid, true);
            
            var cart = CartConverter.ToCartItemDTOs(cartItems.ToList());

            return cart;
        }

        public async Task<bool> RemoveItemFromCart(string uid, int productId)
        {
            var result = await _cartRepo.RemoveClientItem(uid, productId);
            if (result)
            {
                await _cartRepo.Save();
                return true;
            }
            return false;
        }

        public async Task RemoveItemsFromCart(string uid)
        {
            await _cartRepo.RemoveClientItems(uid);
            await _cartRepo.Save();
        }

        public async Task<bool> UpdateItemInCart(string uid, int productId, int totalAmount = 1)
        {
            var item = await _cartRepo.GetByClientAndProductId(uid, productId, false);
            if (item == null)
            {
                var product = _productsRepo.GetProductById(productId);
                if (product == null)
                {
                    return false;
                }
                await _cartRepo.CreateItem(new CartItem { ClientId = uid, ProductId = productId, Amount = totalAmount });
                await _cartRepo.Save();
                return true;
            }

            item.Amount = totalAmount;
            _cartRepo.UpdateItem(item);
            await _cartRepo.Save();

            return true;
        }
    }
}
