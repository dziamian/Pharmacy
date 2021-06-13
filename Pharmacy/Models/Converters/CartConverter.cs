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
        public static CartItemReadDto ToCartItemDto(CartItem cartItem)
        {
            return new CartItemReadDto { 
                Product = cartItem.Product, 
                Amount = cartItem.Amount, 
                IsAvailable = cartItem.Product != null && cartItem.Product.Supply >= cartItem.Amount 
            };
        }

        public static List<CartItemReadDto> ToCartItemDtos(List<CartItem> cartItems)
        {
            var dtos = new List<CartItemReadDto>();
            cartItems.ForEach((cartItem) =>
            {
                dtos.Add(ToCartItemDto(cartItem));
            });
            return dtos;
        }
    }
}
