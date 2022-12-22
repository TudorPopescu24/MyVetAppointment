using FluentValidation;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
    public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidator()
        {
            RuleFor(x => x.PetId).NotEmpty().NotNull();
            RuleFor(x => x.DoctorId).NotEmpty().NotNull();
            RuleFor(x => x.DateTime).NotEmpty().NotNull();
        }
    }
}
