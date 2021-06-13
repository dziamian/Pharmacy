using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Cart
{
    public class CartItemCreateDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
