using Pharmacy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Pharmacy.Models.Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public bool Supplement { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public long Supply { get; set; }

        [Url]
        [Required]
        [MaxLength(1024)]
        public string Image { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
        [Required]
		public Category Category { get; set; }

		[Required]
		public DateTime CreationDate { get; set; }

		public ICollection<ProductActiveSubstance> ActiveSubstances { get; set; }

        public ICollection<ProductPassiveSubstance> PassiveSubstances { get; set; }

		public ICollection<Rating> Ratings { get; set; }
	}
}
