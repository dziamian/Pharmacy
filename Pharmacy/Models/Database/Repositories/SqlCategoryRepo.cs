using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
	public class SqlCategoryRepo : ICategoryRepo
	{
		private readonly PharmacyDBContext m_context;

		public SqlCategoryRepo(PharmacyDBContext context)
		{
			m_context = context;
		}

		public async Task<bool> CategoryExists(int id)
		{
			return await m_context.Categories.AnyAsync(c => c.Id == id);
		}

		public async Task CreateCategory(Category category)
		{
			await m_context.Categories.AddAsync(category);
		}

		public async Task<IEnumerable<Category>> GetAllCategories()
		{
			return await m_context.Categories.ToListAsync();
		}

		public async Task<Category> GetCategoryById(int id)
		{
			return await m_context.Categories.FirstOrDefaultAsync(c => c.Id == id);
		}

		public void MarkForUpdate(Category category)
		{
			var entry = m_context.Entry(category);
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
