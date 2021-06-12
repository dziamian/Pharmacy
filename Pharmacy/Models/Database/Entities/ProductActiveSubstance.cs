using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Models.Database.Entities
{
	public class ProductActiveSubstance
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }
		[Required]
		public Product Product { get; set; }

		[Required]
		[ForeignKey(nameof(ActiveSubstance))]
		public int ActiveSubstanceId { get; set; }
		[Required]
		public ActiveSubstance ActiveSubstance { get; set; }

		[Required]
		public int Dose { get; set; }
	}
}
