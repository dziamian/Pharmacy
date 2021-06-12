using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
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
    }
}
