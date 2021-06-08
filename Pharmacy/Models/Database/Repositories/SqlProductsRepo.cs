using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
    public class SqlProductsRepo : IProductsRepo
    {
        private readonly PharmacyDBContext _context;

        public SqlProductsRepo(PharmacyDBContext context)
        {
            _context = context;
        }

		public void CreateProduct(Product product)
		{
            product.CreationDate = DateTime.Now;
            _context.Add(product);
		}

		public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault((product) => product.Id == id);
        }

		public int SaveChanges()
		{
            return _context.SaveChanges();
		}

		public void UpdateProduct(Product product)
		{
		}
	}
}
