using Pharmacy.Models.Data_Transfrom_Objects.Substance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Product
{
	public class ProductCreateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Cost { get; set; }

        [Url]
        [MaxLength(1024)]
        public string Image { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
		public int CategoryId { get; set; }

		[Required]
		public IEnumerable<DoseCreateDto> ActiveSubstances { get; set; }

        [Required]
        public IEnumerable<DoseCreateDto> PassiveSubstances { get; set; }
    }
}
