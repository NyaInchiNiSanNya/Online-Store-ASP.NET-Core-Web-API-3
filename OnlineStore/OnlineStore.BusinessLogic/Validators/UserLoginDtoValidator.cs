using FluentValidation;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Validators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().Length(4, 20).NotNull();
            RuleFor(x => x.Password).Matches(Patterns.PasswordPattern).NotNull();
        }
    }
}
