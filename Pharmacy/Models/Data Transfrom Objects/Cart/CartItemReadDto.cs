using Pharmacy.Models.Data_Transfrom_Objects.Product;
using Pharmacy.Models.Database.Entities;

namespace Pharmacy.Models.Data_Transfrom_Objects.Cart
{
    public class CartItemReadDto
    {
        public ProductReadDto Product { get; set; }
        public int Amount { get; set; }
        public bool IsAvailable { get; set; }
    }
}
