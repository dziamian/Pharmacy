using Pharmacy.Models.Data_Transfrom_Objects.Substances;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Products
{
    public class ProductReadDto
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public long Supply { get; set; }

        [Url]
        [MaxLength(1024)]
        public string Image { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
		public int CategoryId { get; set; }

		public IEnumerable<SubstanceDoseDto> ActiveSubstances { get; set; }

		public IEnumerable<SubstanceDoseDto> PassiveSubstances { get; set; }

		public IEnumerable<int> Ratings { get; set; }
    }
}
