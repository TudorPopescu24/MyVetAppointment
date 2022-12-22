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
	}
}
