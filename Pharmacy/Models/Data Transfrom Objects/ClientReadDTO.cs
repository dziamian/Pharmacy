using FluentValidation.Attributes;
using Pharmacy.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects
{
    [Validator(typeof(ClientReadDTOValidator))]
    public class ClientReadDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
