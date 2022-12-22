using FluentValidation;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
    public class CreatePromotionDtoValidator : AbstractValidator<CreatePromotionDto>
    {
        public CreatePromotionDtoValidator() {

            RuleFor(x => x.ClinicId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
  
        }
    }
}
