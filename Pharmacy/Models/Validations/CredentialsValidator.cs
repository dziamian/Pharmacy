using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Pharmacy.Models.Data_Transform_Objects;

namespace Pharmacy.Models.Validations
{
    public class CredentialsValidator : AbstractValidator<CredentialsDTO>
    {
        public CredentialsValidator()
        {
            RuleFor(dto => dto.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(dto => dto.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(dto => dto.Password).Length(6, 12).WithMessage("Password must be between 6 and 12 characters");
        }
    }
}
