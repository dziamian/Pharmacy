using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Database.Entities;
using Pharmacy.Models.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Repositories
{
	public class SqlOrdersRepo : IOrdersRepo
	{
		private readonly PharmacyDBContext m_context;

		public SqlOrdersRepo(PharmacyDBContext context)
		{
			m_context = context;
		}

		public async Task<bool> ContainsTransaction(string transactionId)
		{
			return (await m_context.Orders.FirstOrDefaultAsync(order => order.TransactionId.Equals(transactionId))) != null;
		}

		public async Task CreateOrder(Order order)
		{
			if (order == null)
			{
				return;
			}

			await m_context.Orders.AddAsync(order);
		}

		public async Task<Order> GetOrderById(int id)
		{
			return await m_context.Orders.FirstOrDefaultAsync(order => order.Id == id);
		}

		public async Task<IEnumerable<Order>> GetOrdersForUser(string userId)
		{
			if (userId == null)
			{
				return null;
			}

			return await m_context.Orders.Where(order => order.ClientId.Equals(userId)).ToListAsync();
		}

		public void MarkForUpdate(Order order)
		{
			if (order == null)
			{
				return;
			}

			m_context.Entry(order).State = EntityState.Modified;
		}

		public async Task<int> SaveChanges()
		{
			return await m_context.SaveChangesAsync();
		}
	}
}
