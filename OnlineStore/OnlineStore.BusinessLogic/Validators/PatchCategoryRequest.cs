using FluentValidation;
using OnlineStore.BusinessLogic.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Validators
{

    public class PatchCategoryValidator : AbstractValidator<PatchCategoryRequest>
    {
        public PatchCategoryValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1);
            RuleFor(x => x.newName).NotNull().NotEmpty();
        }
    }
}
