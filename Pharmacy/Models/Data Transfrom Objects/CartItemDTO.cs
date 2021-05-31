using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects
{
    public class CartItemDTO
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
