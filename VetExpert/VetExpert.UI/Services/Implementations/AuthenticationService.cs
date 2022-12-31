using System.Net.Http.Json;
using VetExpert.Domain.Dto;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		private const string ApiURL = "https://localhost:7231/api/Authentication/login";
		private readonly HttpClient httpClient;

		public AuthenticationService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<string> Login(UserLoginDto user)
		{
			var result = await httpClient.PostAsJsonAsync(ApiURL, user);

			var token = await result.Content.ReadAsStringAsync();

			return token;
		}
	}
}
