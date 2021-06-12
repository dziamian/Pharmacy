using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
	public interface ICategoryRepo
	{
		Task CreateCategory(Category category);
		Task<IEnumerable<Category>> GetAllCategories();
		Task<Category> GetCategoryById(int id);
		void MarkForUpdate(Category category);
		Task<bool> CategoryExists(int id);
		Task SaveChanges();
	}
}
