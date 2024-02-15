using FluentValidation;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using OnlineStore.BusinessLogic.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationRequest>
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
