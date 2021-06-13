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
			m_context.Products.Include(p => p.ActiveSubstances).Include(p => p.PassiveSubstances);
			await m_context.Products.AddAsync(product);
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			return await m_context.Products
				.Include(p => p.ActiveSubstances).ThenInclude(p => p.ActiveSubstance)
				.Include(p => p.PassiveSubstances).ThenInclude(p => p.PassiveSubstance)
				.Include(p => p.Ratings)
				.ToListAsync();
		}

		public async Task<Product> GetProductById(int id)
		{
			return await m_context.Products
				.Where(p => p.Id == id)
				.Include(p => p.ActiveSubstances).ThenInclude(p => p.ActiveSubstance)
				.Include(p => p.PassiveSubstances).ThenInclude(p => p.PassiveSubstance)
				.Include(p => p.Ratings)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<Product>> GetSubstitutes(int id)
		{
			var product = (await GetProductById(id));
			if (product == null)
			{
				return new Product[0];
			}

			var products = m_context.Products
				.Include(p => p.ActiveSubstances)
				.Where(p => !p.Supplement && p.ActiveSubstances.Count == product.ActiveSubstances.Count)
				.Include(p => p.ActiveSubstances).ThenInclude(p => p.ActiveSubstance);	

			return products.Where(p => product.IsSubstitutedBy(p, 0.05f));
		}

		public void MarkForUpdate(Product product)
		{
			var entry = m_context.Entry(product);
			if (entry != null)
			{
				entry.State = EntityState.Modified;
			}
		}

		public async Task SaveChanges()
		{
            await m_context.SaveChangesAsync();
		}
	}
}
