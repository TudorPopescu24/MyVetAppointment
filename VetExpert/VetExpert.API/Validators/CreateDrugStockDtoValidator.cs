using FluentValidation;
using Microsoft.AspNetCore.Identity;
using VetExpert.API.Dto;


namespace VetExpert.API.Validators
{
    public class CreateDrugStockDtoValidator : AbstractValidator<CreateDrugStockDto>
    {

        public CreateDrugStockDtoValidator()
        {
            RuleFor(x=>x.DrugId).NotEmpty().NotNull();
            RuleFor(x => x.Quantity).NotNull();
            RuleFor(x => x.ExpirationDate).NotEmpty().NotNull();

        }

    }
}
