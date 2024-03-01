using FluentValidation;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Validators
{
    public class PaginationDtoValidator : AbstractValidator<PaginationDto>
    {
        public PaginationDtoValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).LessThanOrEqualTo(50);
        }
    }
}
