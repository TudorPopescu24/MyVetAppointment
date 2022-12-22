using FluentValidation;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
    public class CreateBillDtoValidator : AbstractValidator<CreateBillDto>
    {
        public CreateBillDtoValidator()
        {
            RuleFor(x => x.Value).NotNull();
            RuleFor(x => x.Currency).NotEmpty().NotNull();
            RuleFor(x => x.DateTime).NotEmpty().NotNull();
        }
    }
}
