using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Models.Database.Entities
{
	public class PassiveSubstance { 

        [Key]
		public int Id { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Name { get; set; }

        public ICollection<ProductPassiveSubstance> Products { get; set; }

		public override bool Equals([AllowNull] object obj)
		{
			return obj is PassiveSubstance tmp && this.Equals(tmp);
		}

		public bool Equals([DisallowNull] PassiveSubstance obj)
		{
			return obj.Id == this.Id;
		}

		public override int GetHashCode()
		{
			return this.Id % 16;
		}
	}
}
