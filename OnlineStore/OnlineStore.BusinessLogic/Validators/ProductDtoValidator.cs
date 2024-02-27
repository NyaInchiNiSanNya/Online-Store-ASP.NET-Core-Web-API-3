using FluentValidation;
using OnlineStore.Data.Entities;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleForEach(x => x.CategoriesId)
                .Must(x => x > 0);
        }
    }
}
