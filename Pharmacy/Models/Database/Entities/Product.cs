using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        [Required]
        [MaxLength(1023)]
        public string Image { get; set; }

        [Required]
        [MaxLength(1023)]
        public string Description { get; set; }
    }
}
