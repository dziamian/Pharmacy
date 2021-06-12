using Pharmacy.Models.Data_Transfrom_Objects.Client;
using Pharmacy.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Converters
{
    public static class ClientsConverter
    {
        public static ClientReadDto ToClientReadDto(Client client)
        {
            return new ClientReadDto { 
                Name = client.Name, 
                Email = client.Email, 
                Phone = client.Phone, 
                DateOfBirth = client.DateOfBirth.Value.ToString("yyyy-MM-dd"), 
                Gender = client.Gender 
            };
        }

        public static Client FromClientCreateDto(ClientCreateDto dto, string uid)
        {
            return new Client { 
                ClientId = uid, 
                Name = dto.Name, 
                Email = dto.Email, 
                Phone = dto.Phone, 
                DateOfBirth = dto.DateOfBirth, 
                Gender = dto.Gender 
            };
        }
    }
}
