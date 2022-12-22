using FluentValidation;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
    public class CreateDoctorDtoValidator : AbstractValidator<CreateDoctorDto>
    {
        public CreateDoctorDtoValidator()
        {
            RuleFor(x => x.ClinicId).NotEmpty().NotNull();
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}
