using Microsoft.AspNetCore.Components;
using System;
using VetExpert.Domain;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.Users
{
    public partial class IndexPageBase : ComponentBase
    {
        [Inject]
        private IUserService UserService { get; set; } = default!;

        protected List<User>? Users { get; set; } = null;

        protected bool ShowUserForm { get; set; } = false;
		protected bool ShowPetForm { get; set; } = false;

        protected User User { get; set; } = new User();

		protected Pet Pet { get; set; } = new Pet();

        protected bool IsNewEntity { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            await ReadUsersAsync();
		}

        protected void OnAddButtonClick()
        {
            User = new User();
			IsNewEntity = true;
			ShowUserForm = true;
		}

		protected void OnAddPetClick(User user)
		{
			Pet = new Pet
			{
				UserId = user.Id,
				User = user
			};

			ShowPetForm = true;
		}

		protected void OnEditButtonClick(User editUser)
		{
			User = new User
			{
				Id = editUser.Id,
				Name = editUser.Name,
				Email = editUser.Email,
				PhoneNumber = editUser.PhoneNumber,
				Address = editUser.Address,
				Pets = editUser.Pets,

			};
			IsNewEntity = false;
			ShowUserForm = true;
		}

		protected void OnCancelButtonClick()
        {
			ShowUserForm = false;
		}

		protected async Task OnValidSubmitAsync()
        {
			if (IsNewEntity)
			{
				await UserService.InsertUser(User);
			}
			else
			{
				await UserService.UpdateUser(User);
			}

			ShowUserForm = false;

			await ReadUsersAsync();
		}

		protected async Task OnDeleteAsync(User deleteUser)
		{
			await UserService.DeleteUser(deleteUser.Id);

			await ReadUsersAsync();
		}

		protected void OnPetCancelButtonClick()
		{
			ShowPetForm = false;
		}

		protected async Task OnPetValidSubmitAsync()
		{
			await PetService.InsertPet(Pet);

			ShowPetForm = false;
		}


		private async Task ReadUsersAsync()
        {
            Users = (await UserService.GetAllUsers()).ToList();
        }

    }
}
