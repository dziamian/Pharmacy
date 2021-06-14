using FluentValidation;
using Pharmacy.Models.Data_Transfrom_Objects.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Validators
{
    public class ClientUpdateDTOValidator : AbstractValidator<ClientUpdateDto>
    {
        public ClientUpdateDTOValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name cannot be empty.");
        }
    }
}
