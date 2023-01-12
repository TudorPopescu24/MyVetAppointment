using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Linq;
using VetExpert.Domain;
using VetExpert.Domain.Dto;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.Authentication.Login
{
	public class IndexPageBase : ComponentBase
	{
		[Inject]
		private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

		[Inject] 
		private ILocalStorageService LocalStorageService { get; set; } = default!;

		[Inject]
		private IAuthenticationService AuthenticationService { get; set; } = default!;

		[Inject]
		private NavigationManager NavigationManager { get; set; } = default!;


		protected UserLoginDto User = new UserLoginDto();

		protected bool HasError { get; set; } = false;

		protected string ErrorMessage { get; set; } = string.Empty;

		protected async Task HandleLogin()
		{
			(bool responseSuccess, string responseMessage) = await AuthenticationService.Login(User);

			if (responseSuccess)
			{
				HasError = false;
				ErrorMessage = string.Empty;

				await LocalStorageService.SetItemAsync("token", responseMessage);

				var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

				if (authState.User.HasClaim(ClaimTypes.Role, UserRole.Admin))
				{
					NavigationManager.NavigateTo("admin/clinics");
				}
				else if (authState.User.HasClaim(ClaimTypes.Role, UserRole.User))
				{
					NavigationManager.NavigateTo("user/pets");
				}
				else if (authState.User.HasClaim(ClaimTypes.Role, UserRole.Clinic))
				{
					NavigationManager.NavigateTo("clinic/appointments");
				}
			}
			else
			{
				HasError = true;
				ErrorMessage = responseMessage;
			}
		}
	}
}
