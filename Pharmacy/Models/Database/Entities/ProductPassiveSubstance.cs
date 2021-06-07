using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Models.Database.Entities
{
	public class ProductPassiveSubstance
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }
		[Required]
		public Product Product { get; set; }

		[Required]
		[ForeignKey(nameof(PassiveSubstance))]
		public int PassiveSubstanceId { get; set; }
		[Required]
		public PassiveSubstance PassiveSubstance { get; set; }

		[Required]
		public int Dose { get; set; }
	}
}
