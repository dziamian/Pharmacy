using Pharmacy.Models.Data_Transfrom_Objects.Order;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Converters
{
	public class OrderConverter
	{
		public static OrderReadDto ToOrderReadDto(Order order)
		{
			var items = new List<OrderItemReadDto>();

			OrderReadDto dto = new OrderReadDto
			{
				Id = order.Id,
				CreationDate = order.CreationDate ?? DateTime.Now,
				CompletionDate = order.CompletionDate,
				Items = items,
				TotalCost = 0
			};

			foreach (var it in order.Products)
			{
				items.Add(new OrderItemReadDto
				{
					ProductId = it.Product.Id,
					ProductName = it.Product.Name,
					ProductCost = it.Product.Cost,
					Amount = it.Amount
				});
			}

			foreach (var it in dto.Items)
			{
				dto.TotalCost += it.ProductCost * it.Amount;
			}

			return dto;
		}

		public static IEnumerable<OrderReadDto> ToOrderReadDtos(IEnumerable<Order> orders)
		{
			if (orders == null)
			{
				return null;
			}

			ICollection<OrderReadDto> collection = new List<OrderReadDto>();

			foreach (var it in orders)
			{
				collection.Add(ToOrderReadDto(it));
			}

			return collection;
		}

	}
}
