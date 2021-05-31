using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Database.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public string ClientId { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
