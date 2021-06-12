using Microsoft.EntityFrameworkCore;
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
        private readonly PharmacyDBContext m_context;

        public SqlProductsRepo(PharmacyDBContext context)
        {
            m_context = context;
        }

		public async Task CreateProduct(Product product)
		{
			m_context.Products.Include("Products.ActiveSubstances").Include("Products.PassiveSubstances");
			await m_context.Products.AddAsync(product);
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			return await m_context.Products
				.Include(p => p.ActiveSubstances)
				.Include(p => p.PassiveSubstances)
				.Include(p => p.Ratings)
				.ToListAsync();
		}

		public async Task<Product> GetProductById(int id)
		{
			return await m_context.Products
				.Include(p => p.ActiveSubstances)
				.Include(p => p.PassiveSubstances)
				.Include(p => p.Ratings)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public void MarkForUpdate(Product product)
		{
			var entry = m_context.Entry(product);
			if (entry != null)
			{
				entry.State = EntityState.Modified;
			}
		}

		public int SaveChanges()
		{
            return m_context.SaveChanges();
		}
	}
}
