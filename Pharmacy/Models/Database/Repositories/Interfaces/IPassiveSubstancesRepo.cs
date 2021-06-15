using Pharmacy.Models.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
	public interface IPassiveSubstancesRepo
	{
		Task<PassiveSubstance> CreatePassiveSubstance(PassiveSubstance activeSubstance);
		Task<IEnumerable<PassiveSubstance>> GetAllPassiveSubstances();
		Task<PassiveSubstance> GetActiveSubstanceById(int id);
		Task<bool> PassiveSubstanceExists(int id);
		void MarkForUpdate(PassiveSubstance activeSubstance);
		Task SaveChanges();
	}
}
