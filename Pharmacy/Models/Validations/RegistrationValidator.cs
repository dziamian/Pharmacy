using FluentValidation;
using Pharmacy.Models.Data_Transform_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Validations
{
    public class RegistrationValidator : AbstractValidator<RegistrationDTO>
    {
        public RegistrationValidator()
        {
            RuleFor(dto => dto.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(dto => dto.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name cannot be empty");
        }
    }
}
