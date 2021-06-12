using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Models.Database.Entities
{
	public class Rating
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Client))]
		public string ClientId { get; set; }
		[Required]
		public Client Client { get; set; }

		[Required]
		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }
		[Required]
		public Product Product { get; set; }

		[Required]
		public int Score { get; set; }
	}
}
