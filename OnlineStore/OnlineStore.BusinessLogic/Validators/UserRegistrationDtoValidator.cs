using FluentValidation;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Validators
{
    public class RegistrationValidator : AbstractValidator<UserRegistrationDto>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Matches(Patterns.NamePattern);

            RuleFor(x => x.Email)
                .EmailAddress()
                .Length(4, 20)
                .NotNull();

            RuleFor(x => x.Password)
                .NotNull()
                .Matches(Patterns.PasswordPattern);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password);
        }
    }
}
