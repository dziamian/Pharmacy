using Pharmacy.Models.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
	public interface IActiveSubstancesRepo
	{
		Task<ActiveSubstance> CreateActiveSubstance(ActiveSubstance activeSubstance);
		Task<IEnumerable<ActiveSubstance>> GetAllActiveSubstances();
		Task<ActiveSubstance> GetActiveSubstanceById(int id);
		void MarkForUpdate(ActiveSubstance activeSubstance);
		Task<bool> ActiveSubstanceExists(int id);
		Task SaveChanges();
	}
}
