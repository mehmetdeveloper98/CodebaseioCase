using Customer.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Validations
{
    public sealed class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email must be filled");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be filled");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");

            RuleFor(x => x.Password).NotNull().WithMessage("Password must be filled");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be filled");
            RuleFor(x => x.Password).Matches("[A-Z]").WithMessage("Password must contain a big letter");
            RuleFor(x => x.Password).Matches("[a-z]").WithMessage("Password must contain a small letter");
            RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Password must contain a number");
            RuleFor(x => x.Password).Matches("[^a-zA-Z0-9]").WithMessage("Password must contain a special character");
        }
    }
}
