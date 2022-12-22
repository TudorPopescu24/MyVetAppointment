using FluentValidation;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
    public class CreateDrugDtoValidator : AbstractValidator<CreateDrugDto>
    {
        public CreateDrugDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Manufacturer).NotEmpty().NotNull();
            RuleFor(x => x.Weight).NotNull();
            RuleFor(x => x.Prospect).NotEmpty().NotNull();
            RuleFor(x => x.Price).NotNull();
        }
    }
}
