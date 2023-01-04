using Microsoft.AspNetCore.Components;
using System;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClientUser.Pets
{
    public partial class IndexPageBase : ComponentBase
    {

		[Inject]
		private IPetService PetService { get; set; }
		protected List<Pet>? Pets { get; set; } = null;

		protected Pet Pet { get; set; } = new Pet();

		protected bool IsNewEntity { get; set; } = false;

		protected bool ShowPetForm { get; set; } = false;



		protected async override Task OnInitializedAsync()
		{
			await ReadPetsAsync();
		}

		private async Task ReadPetsAsync()
        {
			Pets = (await PetService.GetAllPets()).ToList();
		}

		protected void OnEditButtonClick(Pet editPet)
		{
			Pet = new Pet
			{
				Id = editPet.Id,
				Name = editPet.Name,
				TypeOfPet = editPet.TypeOfPet,
				Weight = editPet.Weight,
				Age= editPet.Age,
				IsVaccinated= editPet.IsVaccinated,
				DateOfVaccine= editPet.DateOfVaccine
			};
			IsNewEntity = false;
			ShowPetForm = true;
		}

		protected async Task OnValidSubmitAsync()
		{
			
				await PetService.UpdatePet(Pet);
			

			ShowPetForm = false;

			await ReadPetsAsync();
		}

		protected async Task OnDeleteAsync(Pet deletePet)
		{
			await PetService.DeletePet(deletePet.Id);

			await ReadPetsAsync();
		}

		protected void OnCancelButtonClick()
		{
			ShowPetForm = false;
		}
	}
}
