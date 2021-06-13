using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
	public class SqlPassiveSubstancesRepo : IPassiveSubstancesRepo
	{
		private readonly PharmacyDBContext m_context;

		public SqlPassiveSubstancesRepo(PharmacyDBContext context)
		{
			m_context = context;
		}

		public async Task CreatePassiveSubstance(PassiveSubstance activeSubstance)
		{
			await m_context.PassiveSubstances.AddAsync(activeSubstance);
		}

		public async Task<PassiveSubstance> GetActiveSubstanceById(int id)
		{
			return await m_context.PassiveSubstances
				.Include(p => p.Products)
				.FirstOrDefaultAsync(substance => substance.Id == id);
		}

		public async Task<IEnumerable<PassiveSubstance>> GetAllPassiveSubstances()
		{
			return await m_context.PassiveSubstances
				.Include(p => p.Products)
				.ToListAsync();
		}

		public void MarkForUpdate(PassiveSubstance activeSubstance)
		{
			var entry = m_context.Entry(activeSubstance);
			if (entry != null)
			{
				entry.State = EntityState.Modified;
			}
		}

		public async Task<bool> PassiveSubstanceExists(int id)
		{
			return await m_context.PassiveSubstances.AnyAsync(p => p.Id == id);
		}

		public async Task SaveChanges()
		{
			await m_context.SaveChangesAsync();
		}
	}
}
