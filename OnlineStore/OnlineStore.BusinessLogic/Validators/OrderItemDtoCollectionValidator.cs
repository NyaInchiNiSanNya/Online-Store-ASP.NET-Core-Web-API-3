using FluentValidation;
using OnlineStore.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Validators
{
    public class OrderItemDtoCollectionValidator : AbstractValidator<List<OrderItemDto>>
    {
        public OrderItemDtoCollectionValidator()
        {
            RuleForEach(x => x)
                .SetValidator(new OrderItemDtoValidator());
        }
    }
}
