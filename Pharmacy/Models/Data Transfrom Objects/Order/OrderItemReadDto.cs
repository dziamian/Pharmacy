using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Order
{
	public class OrderItemReadDto
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public int ProductCost { get; set; }

		public int Amount { get; set; }
	}
}
