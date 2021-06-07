using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models.Database.Entities
{
	public class ActiveSubstance
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(1024)]
		public string Name { get; set; }

		public ICollection<ProductActiveSubstance> Products { get; set; }
	}
}
