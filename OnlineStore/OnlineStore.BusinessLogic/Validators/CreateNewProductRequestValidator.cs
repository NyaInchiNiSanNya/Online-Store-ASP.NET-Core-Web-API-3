using FluentValidation;
using OnlineStore.BusinessLogic.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Validators
{
    public class CreateNewProductRequestValidator : AbstractValidator<CreateNewProductRequest>
    {
        public CreateNewProductRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleForEach(x => x.CategoriesId)
                .Must(x => x > 0);

        }
    }
    
}
