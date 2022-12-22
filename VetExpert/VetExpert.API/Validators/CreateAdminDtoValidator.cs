using FluentValidation;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
    public class CreateAdminDtoValidator : AbstractValidator<CreateAdminDto>
    {
        public CreateAdminDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull();
        }
    }
}
