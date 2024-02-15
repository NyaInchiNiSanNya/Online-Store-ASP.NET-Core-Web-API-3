using FluentValidation;
using OnlineStore.BusinessLogic.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Validators
{
    public class PatchProductRequestValidator : AbstractValidator<PatchProductRequest>
    {
        public PatchProductRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1);
            RuleFor(x => x.NewName).NotNull();
            RuleFor(x => x.NewDescription);
            RuleFor(x => x.NewPrice).GreaterThanOrEqualTo(0);
        }
    }
}
