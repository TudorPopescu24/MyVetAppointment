using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClientUser.Pets
{
    public partial class IndexPageBase : ComponentBase
    {

	    [Inject] 
	    private IPetService PetService { get; set; } = default!;

		[Inject]
		private IUserService UserService { get; set; } = default!;

		[Inject]
		private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

		protected Guid CurrentUserId { get; set; } = Guid.Empty;

		protected List<Pet>? Pets { get; set; } = null;

		protected Pet Pet { get; set; } = new Pet();

		protected bool IsNewEntity { get; set; } = false;

		protected bool ShowPetForm { get; set; } = false;


		protected async override Task OnInitializedAsync()
		{
			await GetCurrentUserId();
			await ReadPetsAsync();
		}

		protected void OnAddButtonClick()
		{
			Pet = new Pet
			{
				TypeOfPet = nameof(PetType.Dog),
				UserId = CurrentUserId
			};
			IsNewEntity = true;
			ShowPetForm = true;
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
				DateOfVaccine= editPet.DateOfVaccine,
				UserId = CurrentUserId
			};
			IsNewEntity = false;
			ShowPetForm = true;
		}

		protected async Task OnValidSubmitAsync()
		{
			if (IsNewEntity)
			{
				await PetService.InsertPet(Pet);
			}
			else
			{
				await PetService.UpdatePet(Pet);
			}

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

		private async Task GetCurrentUserId()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var applicationUserId = authState.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => new Guid(x.Value)).FirstOrDefault();

			var user = await UserService.GetByAppUserId(applicationUserId);
			CurrentUserId = user.Id;
		}

		private async Task ReadPetsAsync()
		{
			Pets = (await PetService.GetClientPets(CurrentUserId)).ToList();
		}
	}
}
