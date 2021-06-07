using Pharmacy.Models.Data_Transfrom_Objects;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
    public interface ICartRepo
    {
        Task<IEnumerable<CartItem>> GetByClientId(string uid, bool included);
        Task<IEnumerable<CartItem>> GetByProductId(int productId, bool included);
        Task<CartItem> GetByClientAndProductId(string uid, int productId, bool included);
        Task CreateItem(CartItem item);
        Task<bool> RemoveClientItem(string uid, int productId);
        Task RemoveClientItems(string uid);
        void UpdateItem(CartItem item);
        Task Save();
    }
}
