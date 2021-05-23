using AutoMapper;
using Pharmacy.Models.Data_Transform_Objects;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Mappings
{
    public class AccountMapping : Profile
    {
        public AccountMapping()
        {
            CreateMap<RegistrationDTO, Client>().ForMember(au => au.UserName, map => map.MapFrom(dto => dto.Email));
        }
    }
}
