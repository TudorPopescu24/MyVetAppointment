using FluentValidation;
using FluentValidation.Validators;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
    public class CreateSpecializationDtoValidator: AbstractValidator<CreateSpecializationDto>
    {
        public CreateSpecializationDtoValidator() {

            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
}
