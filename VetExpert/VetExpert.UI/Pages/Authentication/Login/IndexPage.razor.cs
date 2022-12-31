using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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

		protected UserLoginDto User = new UserLoginDto();

		protected async Task HandleLogin()
		{
			var token = await AuthenticationService.Login(User);

			await LocalStorageService.SetItemAsync("token", token);

			await AuthenticationStateProvider.GetAuthenticationStateAsync();
		}
	}
}
