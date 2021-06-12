using Pharmacy.Models.Database.Entities;

namespace Pharmacy.Models.Data_Transfrom_Objects
{
    public class CartItemDTO
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
        public bool IsAvailable { get; set; }
    }
}
