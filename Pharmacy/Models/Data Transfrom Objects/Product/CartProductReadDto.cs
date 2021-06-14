using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Product
{
    public class CartProductReadDto
    {
        [Required]
        public int Id { get; set; }
       
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Cost { get; set; }
        
        [Url]
        [MaxLength(1024)]
        public string Image { get; set; }
    }
}
