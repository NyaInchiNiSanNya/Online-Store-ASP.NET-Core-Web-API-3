using FluentValidation;
using OnlineStore.BusinessLogic.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Validators
{
    public class CreateNewCategoryRequestValidator : AbstractValidator<CreateNewCategoryRequest>
    {
        public CreateNewCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();

        }
    }
}
