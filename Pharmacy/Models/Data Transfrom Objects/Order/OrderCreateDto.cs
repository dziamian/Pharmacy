using Pharmacy.Helpers;
using Pharmacy.Models.Data_Transfrom_Objects.Address;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Order
{
	public class OrderCreateDto
	{
		[AllowNull]
		public AddressCreateDto BillingAddress { get; set; }

		[Required]
		public string RecipientName { get; set; }

		[Required]
		public AddressCreateDto ShippingAddress { get; set; }

		[ExternalApiItem(ApiName = "paypal")]
		[Required]
		public string TransactionId { get; set; }

		public string Notes { get; set; }
	}
}
