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
		[ForeignKey(nameof(Address))]
		public int AddressId { get; set; }
		[Required]
		public Address Address { get; set; }

		[Required]
		public string TransactionId { get; set; }

		[Column(TypeName = "Date")]
		public DateTime? CreationDate { get; set; }

		[Column(TypeName = "Date")]
		public DateTime? CompletionDate { get; set; }

		public string Notes { get; set; }

		public ICollection<OrderProduct> Products { get; set; }
	}
}
