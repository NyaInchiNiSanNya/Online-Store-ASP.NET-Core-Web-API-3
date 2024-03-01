using FluentValidation;
using OnlineStore.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Validators
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ProductId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
        }
    }
}
