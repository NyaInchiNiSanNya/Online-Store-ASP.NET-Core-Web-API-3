using FluentValidation;
using OnlineStore.BusinessLogic.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Validators
{
    public class PageValidator : AbstractValidator<GetProductsByPageRequest>
    {
        public PageValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
        }
    }
}
