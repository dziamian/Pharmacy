using FluentValidation;
using Pharmacy.Models.Data_Transfrom_Objects.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Validators
{
    public class ClientCreateDTOValidator : AbstractValidator<ClientCreateDto>
    {
        public ClientCreateDTOValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(dto => dto.Email).EmailAddress().WithMessage("Not valid email address.");
        }
    }
}
