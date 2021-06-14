using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
    public interface IProductsRepo
    {
        Task CreateProduct(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetNewestProducts(int count);
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetSubstitutes(int id);
        void MarkForUpdate(Product product);
        Task SaveChanges();
    }
}
