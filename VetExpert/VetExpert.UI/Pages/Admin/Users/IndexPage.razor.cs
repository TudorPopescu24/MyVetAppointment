using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using VetExpert.Domain;
using VetExpert.Domain.Dto;
using VetExpert.UI.Dto;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.Admin.Users
{
    public partial class IndexPageBase : ComponentBase
    {
        [Inject]
        private IUserService UserService { get; set; } = default!;

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; } = default!;

		[Inject]
        private IMapper Mapper { get; set; } = default!;

        protected List<User>? Users { get; set; } = null;

        protected bool ShowUserForm { get; set; } = false;
        protected CreateUserDto User { get; set; } = new CreateUserDto();

        protected bool IsNewEntity { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            await ReadUsersAsync();
		}

        protected void OnAddButtonClick()
        {
            User = new CreateUserDto();
			IsNewEntity = true;
			ShowUserForm = true;
		}

		protected void OnEditButtonClick(User editUser)
		{
			User = new CreateUserDto
			{
				Id = editUser.Id,
				Name = editUser.Name,
				Email = editUser.Email,
				PhoneNumber = editUser.PhoneNumber,
				Address = editUser.Address
			};
            User.UserName = User.Password = User.ConfirmPassword = "Password"; //no validation error
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
                var appUserId = await CreateApplicationUser();

                var user = Mapper.Map<User>(User);
				user.ApplicationUserId = appUserId;

                await UserService.InsertUser(user);
			}
			else
			{
                var user = Mapper.Map<User>(User);
                await UserService.UpdateUser(user);
			}

			ShowUserForm = false;

			await ReadUsersAsync();
		}

		protected async Task OnDeleteAsync(User deleteUser)
		{
			await UserService.DeleteUser(deleteUser.Id);

			await ReadUsersAsync();
		}

		private async Task ReadUsersAsync()
        {
            Users = (await UserService.GetAllUsers()).ToList();
        }

        private async Task<Guid> CreateApplicationUser()
        {
            UserLoginDto userLoginDto = new UserLoginDto
            {
                Username = User.UserName,
                Password = User.Password
            };

            ApplicationUser appUser = await AuthenticationService.RegisterClient(userLoginDto);

            return appUser.Id;
        }
    }
}
