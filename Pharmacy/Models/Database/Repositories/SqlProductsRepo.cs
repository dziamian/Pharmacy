using Microsoft.EntityFrameworkCore;
using Pharmacy.Helpers;
using Pharmacy.Helpers.Selectors;
using Pharmacy.Models.Data_Transfrom_Objects.Product;
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

		public async Task<IEnumerable<Product>> GetNewestProducts(int count)
		{
			return await Task.Run(
				() => m_context.Products
					.OrderByDescending(product => product.CreationDate)
					.Take(count)
					.Include(product => product.ActiveSubstances).ThenInclude(substance => substance.ActiveSubstance)
					.Include(product => product.PassiveSubstances).ThenInclude(substance => substance.PassiveSubstance));
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

		public async Task<IEnumerable<Product>> GetSpecificProducts(
			string name,
			int minPrice,
			int maxPrice,
			IEnumerable<int> categories,
			IEnumerable<(int, int)> activeSubstances,
			IEnumerable<(int, int)> passiveSubstances)
		{
			var products = await Task.Run(
				() => m_context.Products
					.Where(product => product.Cost >= minPrice && product.Cost <= maxPrice)
					.Where(product => name == null || product.Name.ToLower().Contains(name.ToLower()))
					.Where(product => categories.Count() == 0 || categories.Contains(product.CategoryId)));

			foreach (var it in activeSubstances)
			{
				products = products.Where(
					p => p.ActiveSubstances.Any(
						activeSubstance =>
							activeSubstance.ActiveSubstanceId == it.Item1 &&
							activeSubstance.Dose == it.Item2));
			}

			foreach (var it in passiveSubstances)
			{
				products = products.Where(
					product => product.PassiveSubstances.Any(
						passiveSubstance =>
							passiveSubstance.PassiveSubstanceId == it.Item1 &&
							passiveSubstance.Dose == it.Item2));
			}

			products = products
				.Include(product => product.ActiveSubstances).ThenInclude(substance => substance.ActiveSubstance)
				.Include(product => product.PassiveSubstances).ThenInclude(substance => substance.PassiveSubstance);

			return products;
		}

		public async Task<IEnumerable<Product>> GetSubstitutes(int id)
		{
			var product = (await GetProductById(id));

			if (product == null)
			{
				return null;
			}

			var products = m_context.Products
				.Include(p => p.ActiveSubstances)
				.Where(p => !p.Supplement && p.ActiveSubstances.Count == product.ActiveSubstances.Count)
				.Include(p => p.ActiveSubstances).ThenInclude(p => p.ActiveSubstance)
				.Include(p => p.PassiveSubstances).ThenInclude(p => p.PassiveSubstance)
				.AsEnumerable();

			var selector = new ProductSubstituteSelector(0.05f);

			return products.Where(p => selector.TestForSubstitution(product, p)).Where(p => p.Id != id);
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
