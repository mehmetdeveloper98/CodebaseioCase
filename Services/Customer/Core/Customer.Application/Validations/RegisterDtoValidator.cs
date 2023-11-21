using Customer.Application.Dtos;
using FluentValidation;

namespace Customer.Application.Validations
{
    public sealed class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email must be filled");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be filled");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");

            RuleFor(x => x.Username).NotNull().WithMessage("Username must be filled");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username must be filled");
            RuleFor(x => x.Username).MinimumLength(3).WithMessage("Username length shoul be greater than 3");

            RuleFor(x => x.Password).NotNull().WithMessage("Password must be filled");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be filled");
            RuleFor(x => x.Password).Matches("[A-Z]").WithMessage("Password must contain a big letter");
            RuleFor(x => x.Password).Matches("[a-z]").WithMessage("Password must contain a small letter");
            RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Password must contain a number");
            RuleFor(x => x.Password).Matches("[^a-zA-Z0-9]").WithMessage("Password must contain a special character");
        }
    }
}
