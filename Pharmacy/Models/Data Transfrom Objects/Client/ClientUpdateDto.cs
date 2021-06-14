using FluentValidation.Attributes;
using Pharmacy.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transfrom_Objects.Client
{
    [Validator(typeof(ClientUpdateDTOValidator))]
    public class ClientUpdateDto
    {
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        
        public string Gender { get; set; }
    }
}
