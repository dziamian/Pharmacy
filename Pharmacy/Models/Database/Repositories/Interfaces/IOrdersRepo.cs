using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories.Interfaces
{
	public interface IOrdersRepo
	{
		Task CreateOrder(Order order);
		Task<IEnumerable<Order>> GetOrdersForUser(string userId);
		Task<Order> GetOrderById(int id);
		void MarkForUpdate(Order order);
		Task<bool> ContainsTransaction(string transactionId);
		Task<int> SaveChanges();
	}
}
