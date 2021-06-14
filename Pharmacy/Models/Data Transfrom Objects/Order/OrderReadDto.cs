using Pharmacy.Models.Data_Transfrom_Objects.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Order
{
	public class OrderReadDto
	{
		public int Id { get; set; }

		public AddressReadDto BillingAddress { get; set; }

		public AddressReadDto ShippingAddress { get; set; }

		public string CompletionDate { get; set; }

		public string CreationDate { get; set; }

		public IEnumerable<OrderItemReadDto> Items { get; set; }

		public int TotalCost { get; set; }
	}
}
