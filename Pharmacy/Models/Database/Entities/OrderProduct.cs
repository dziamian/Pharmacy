using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Models.Database.Entities
{
	public class OrderProduct
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Order))]
		public int OrderId { get; set; }
		[Required]
		public Order Order { get; set; }

		[Required]
		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }
		[Required]
		public Product Product { get; set; }

		[Required]
		public int Amount { get; set; }
	}
}
