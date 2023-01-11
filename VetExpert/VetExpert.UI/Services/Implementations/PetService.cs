using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
	public class PetService : IPetService
	{
		private const string ApiURL = "https://localhost:7231/api/Pets";
		private readonly HttpClient httpClient;

		public PetService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task InsertPet(Pet pet)
		{
			await httpClient.PostAsJsonAsync(ApiURL, pet);
		}

		public async Task UpdatePet(Pet pet)
		{
			await httpClient.PutAsJsonAsync($"{ApiURL}/{pet.Id}", pet);
		}

		public async Task<IEnumerable<Pet>> GetAllPets()
		{
			var result = await httpClient.GetStringAsync(ApiURL);

			return JsonConvert.DeserializeObject<IEnumerable<Pet>>(result);
		}

		public async Task<IEnumerable<Pet>> GetClientPets(Guid userId)
		{
			var apiUrl = $"{ApiURL}/client/{userId}";

			var result = await httpClient.GetStringAsync(apiUrl);

			return JsonConvert.DeserializeObject<IEnumerable<Pet>>(result);
		}

		public async Task DeletePet(Guid petId)
		{
			await httpClient.DeleteAsync($"{ApiURL}/{petId}");
		}
	}
}
