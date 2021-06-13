using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
	public class SqlActiveSubstancesRepo : IActiveSubstancesRepo
	{
		private readonly PharmacyDBContext m_context;

		public SqlActiveSubstancesRepo(PharmacyDBContext context)
		{
			m_context = context;
		}

		public async Task<bool> ActiveSubstanceExists(int id)
		{
			return await m_context.ActiveSubstances.AnyAsync(a => a.Id == id);
		}

		public async Task CreateActiveSubstance(ActiveSubstance activeSubstance)
		{
			await m_context.ActiveSubstances.AddAsync(activeSubstance);
		}

		public async Task<ActiveSubstance> GetActiveSubstanceById(int id)
		{
			return await m_context.ActiveSubstances
				.Include(a => a.Products)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<ActiveSubstance>> GetAllActiveSubstances()
		{
			return await m_context.ActiveSubstances
				.Include(a => a.Products)
				.ToListAsync();
		}

		public void MarkForUpdate(ActiveSubstance activeSubstance)
		{
			var entry = m_context.Entry(activeSubstance);
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
