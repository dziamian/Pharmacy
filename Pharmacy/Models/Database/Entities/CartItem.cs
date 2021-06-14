using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models.Database.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public virtual Client Client { get; set; }
        [Required]
        public string ClientId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
