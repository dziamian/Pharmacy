using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
    public class MockProductsRepo : IProductsRepo
    {
        public Product GetProductById(int id)
        {
            return new Product { Id = 0, Name = "Example", Supplement = true, Cost = 1500, Supply = 1000, Image = "/assets/example.jpg", Description = "Example Description." };
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return new List<Product>
            {
                new Product { Id = 0, Name = "Example", Supplement = true, Cost = 1500, Supply = 1000, Image = "/assets/example.jpg", Description = "Example Description." },
                new Product { Id = 1, Name = "Second Example", Supplement = false, Cost = 500, Supply = 250, Image = "/assets/example.jpg", Description = "Example2 Description." },
                new Product { Id = 2, Name = "Third Example", Supplement = true, Cost = 7000, Supply = 50, Image = "/assets/example.jpg", Description = "Example3 Description." }
            };
        }
    }
}
