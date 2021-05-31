using AutoMapper;
using Pharmacy.Models.Data_Transfrom_Objects;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Mappings
{
    public class CartItemMapping : Profile
    {
        public CartItemMapping()
        {
            CreateMap<CartItem, CartItemDTO>();
        }
    }
}
