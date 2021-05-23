using FluentValidation.Attributes;
using Pharmacy.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Pharmacy.Models.Data_Transform_Objects
{
    [Validator(typeof(CredentialsValidator))]
    public class CredentialsDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
