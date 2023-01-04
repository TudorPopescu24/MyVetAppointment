using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.Domain;
using VetExpert.Domain.Dto;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		private const string ApiURL = "https://localhost:7231/api/Authentication";
		private readonly HttpClient httpClient;

		public AuthenticationService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<(bool, string)> Login(UserLoginDto user)
		{
			var result = await httpClient.PostAsJsonAsync($"{ApiURL}/login", user);

			var token = await result.Content.ReadAsStringAsync();

			return (result.IsSuccessStatusCode, token);
		}

        public async Task<ApplicationUser> RegisterClient(UserLoginDto user)
        {
            var result = await httpClient.PostAsJsonAsync($"{ApiURL}/client/register", user);

			var responseMessage = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ApplicationUser>(responseMessage);
        }

        public async Task<(bool, string)> RegisterClinic(UserLoginDto user)
        {
            var result = await httpClient.PostAsJsonAsync($"{ApiURL}/clinic/register", user);

            var token = await result.Content.ReadAsStringAsync();

            return (result.IsSuccessStatusCode, token);
        }
    }
}
