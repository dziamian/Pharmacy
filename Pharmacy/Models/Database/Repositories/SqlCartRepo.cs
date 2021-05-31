using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Data_Transfrom_Objects;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Pharmacy.Models.Database.Repositories
{
    public class SqlCartRepo : ICartRepo
    {
        private readonly PharmacyDBContext _context;

        public SqlCartRepo(PharmacyDBContext context)
        {
            _context = context;
        }

        public async Task CreateItem(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
        }

        public async Task<CartItem> GetByClientAndProductId(string uid, int productId, bool included = false)
        {
            var cartItems = _context.CartItems;
            if (included)
                return await cartItems.Include(p => p.Product).FirstOrDefaultAsync(x => x.ClientId == uid && x.ProductId == productId);
            return await cartItems.FirstOrDefaultAsync(x => x.ClientId == uid && x.ProductId == productId);
        }

        public async Task<IEnumerable<CartItem>> GetByClientId(string uid, bool included = false)
        {
            var cartItems = _context.CartItems;
            if (included)
                return await cartItems.Include(p => p.Product).Where(x => x.ClientId == uid).ToListAsync();
            return await cartItems.Where(x => x.ClientId == uid).ToListAsync();
        }

        public async Task<IEnumerable<CartItem>> GetByProductId(int productId, bool included = false)
        {
            var cartItems = _context.CartItems;
            if (included)
                return await cartItems.Include(p => p.Product).Where(x => x.ProductId == productId).ToListAsync();
            return await cartItems.Where(x => x.ProductId == productId).ToListAsync();
        }

        public async Task RemoveClientItems(string uid)
        {
            var cartItems = _context.CartItems;
            var clientItems = await GetByClientId(uid);

            await cartItems.BulkDeleteAsync(clientItems);
        }

        public async Task<bool> RemoveClientItem(string uid, int productId)
        {
            var cartItems = _context.CartItems;
            var clientItem = await GetByClientAndProductId(uid, productId);
            if (clientItem == null)
            {
                return false;
            }

            cartItems.Remove(clientItem);
            return true;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateItem(CartItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        /*public async Task<CartItemDTO> AddItem(string uid, int productId, int amount = 1)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(x => x.ClientId == uid && x.ProductId == productId);
            if (item == null)
            {
                var product = _productsRepo.GetProductById(productId);
                _context.AddAsync(new CartItem { ClientId = uid, ProductId = productId, Product = product, Amount = amount });
                return null;
            }

            item.Amount += amount;
            await _context.SaveChangesAsync();
            return await Task.FromResult(_mapper.Map<CartItem, CartItemDTO>(item));
        }*/

        /*public async Task<IEnumerable<CartItemDTO>> GetByClientId(string uid)
        {
            var cartItems = await _context.CartItems.Include(p => p.Product).ToListAsync();
            List<CartItemDTO> cart = _mapper.Map<List<CartItem>, List<CartItemDTO>>(cartItems);
            return cart;
        }

        public async Task<CartItemDTO> UpdateItem(string uid, int productId, int totalAmount = 1)
        {
            throw new NotImplementedException();
        }*/
    }
}
