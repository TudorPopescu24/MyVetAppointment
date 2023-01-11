using VetExpert.UI.Services.Interfaces;
using VetExpert.Domain;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace VetExpert.UI.Services.Implementations
{
    public class UserService : IUserService
    {
        private const string ApiURL = "https://localhost:7231/api/User";
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<User>?> GetAllUsers()
        {
            var result = await httpClient.GetStringAsync(ApiURL);

            return JsonConvert.DeserializeObject<IEnumerable<User>>(result);
        }

        public async Task<User> GetByAppUserId(Guid appUserId)
        {
	        var apiUrl = $"{ApiURL}/applicationUser/{appUserId}";

			var result = await httpClient.GetStringAsync(apiUrl);

			return JsonConvert.DeserializeObject<User?>(result);
		}

		public async Task InsertUser(User user)
        {
            await httpClient.PostAsJsonAsync(ApiURL, user);
        }

        public async Task UpdateUser(User user)
        {
            await httpClient.PutAsJsonAsync($"{ApiURL}/{user.Id}", user);
        }

        public async Task DeleteUser(Guid userId)
        {
            await httpClient.DeleteAsync($"{ApiURL}/{userId}");
        }
	}
}
