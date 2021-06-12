using Pharmacy.Models.Data_Transfrom_Objects;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Converters
{
    public static class CartConverter
    {
        public static CartItemDTO ToCartItemDTO(CartItem cartItem)
        {
            return new CartItemDTO { 
                Product = cartItem.Product, 
                Amount = cartItem.Amount, 
                IsAvailable = cartItem.Product != null && cartItem.Product.Supply >= cartItem.Amount 
            };
        }

        public static List<CartItemDTO> ToCartItemDTOs(List<CartItem> cartItems)
        {
            var dtos = new List<CartItemDTO>();
            cartItems.ForEach((cartItem) =>
            {
                dtos.Add(ToCartItemDTO(cartItem));
            });
            return dtos;
        }
    }
}
