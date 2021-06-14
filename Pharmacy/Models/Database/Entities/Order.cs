using Pharmacy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Models.Database.Entities
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Client))]
		public string ClientId { get; set; }
		[Required]
		public Client Client { get; set; }

		[Required]
		[ForeignKey(nameof(ShippingAddress))]
		public int ShippingAddressId { get; set; }
		[Required]
		public Address ShippingAddress { get; set; }

		[ForeignKey(nameof(BillingAddress))]
		public int? BillingAddressId { get; set; }
		public Address BillingAddress { get; set; }

		[MaxLength(1024)]
		public string RecipientName { get; set; }

		// PayPal transaction identifier
		[Required]
		[ExternalApiItem(ApiName = "paypal")]
		public string TransactionId { get; set; }

		[Required]
		public DateTime CreationDate { get; set; }
		
		public DateTime? CompletionDate { get; set; }

		public string Notes { get; set; }

		public ICollection<OrderProduct> Products { get; set; }
	}
}
