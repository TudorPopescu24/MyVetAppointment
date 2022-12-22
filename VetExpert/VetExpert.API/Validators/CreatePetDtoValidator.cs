using FluentValidation;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
    public class CreatePetDtoValidator : AbstractValidator<CreatePetDto>
    {
        public CreatePetDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.TypeOfPet).NotEmpty().NotNull();
            RuleFor(x => x.Age).NotNull();
            RuleFor(x => x.Weight).NotNull();
            RuleFor(x => x.IsVaccinated).NotNull();

        }
    }
}
