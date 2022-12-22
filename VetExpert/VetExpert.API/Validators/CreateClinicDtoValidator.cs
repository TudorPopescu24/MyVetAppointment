using FluentValidation;
using VetExpert.API.Dto;

namespace VetExpert.API.Validators
{
	public class CreateClinicDtoValidator : AbstractValidator<CreateClinicDto>
	{
		public CreateClinicDtoValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
		}
	}
}
