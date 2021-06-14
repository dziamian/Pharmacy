using Pharmacy.Models.Data_Transfrom_Objects.Address;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Converters
{
    public static class AddressConverter
    {
        public static AddressReadDto ToAddressReadDto(Address address)
        {
            if (address == null)
            {
                return null;
            }
            
            return new AddressReadDto {
                Id = address.Id,
                City = address.City,
                PostalCode = address.PostalCode,
                StreetAndBuildingNo = address.StreetAndBuildingNo,
                LocalNo = address.LocalNo
            };
        }

        public static IEnumerable<AddressReadDto> ToAddressReadDtos(IEnumerable<Address> addresses)
        {
            if (addresses == null)
            {
                return null;
            }

            ICollection<AddressReadDto> collection = new List<AddressReadDto>();

            foreach (var it in addresses)
            {
                collection.Add(ToAddressReadDto(it));
            }

            return collection;
        }
    }
}
