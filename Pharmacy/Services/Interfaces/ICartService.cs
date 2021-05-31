using Pharmacy.Models.Data_Transfrom_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services.Interfaces
{
    public interface ICartService
    {
        public Task<bool> AddItemToCart(string uid, int productId, int amount = 1);
        public Task<bool> UpdateItemInCart(string uid, int productId, int totalAmount = 1);
        public Task<bool> RemoveItemFromCart(string uid, int productId);
        public Task RemoveItemsFromCart(string uid);
        public Task<IEnumerable<CartItemDTO>> GetCart(string uid);
    }
}
