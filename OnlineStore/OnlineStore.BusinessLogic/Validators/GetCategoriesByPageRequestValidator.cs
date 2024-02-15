using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.BusinessLogic.Models.Requests;

namespace OnlineStore.BusinessLogic.Validators
{

    public class CategoriesPageValidator : AbstractValidator<GetCategoriesByPageRequest>
    {
        public CategoriesPageValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
        }
    }
}
