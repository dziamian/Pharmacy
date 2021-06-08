using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
    public interface IProductsRepo
    {
        void CreateProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        int SaveChanges();
        void UpdateProduct(Product product);
    }
}
