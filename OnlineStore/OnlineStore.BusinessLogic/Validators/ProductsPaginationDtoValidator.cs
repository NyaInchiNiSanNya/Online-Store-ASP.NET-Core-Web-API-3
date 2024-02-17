using FluentValidation;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Validators
{

    public class ProductsPaginationDtoValidator : AbstractValidator<ProductsPaginationDto>
    {
        public ProductsPaginationDtoValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
        }
    }
}
