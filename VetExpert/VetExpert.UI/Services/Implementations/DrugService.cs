using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
    public class DrugService : IDrugService
	{
		private const string ApiURL = "https://localhost:7231/api/Drugs";
		private readonly HttpClient httpClient;

		public DrugService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task InsertDrug(Drug drug)
		{
			await httpClient.PostAsJsonAsync(ApiURL, drug);
		}

		public async Task UpdateDrug(Drug drug)
		{
			await httpClient.PutAsJsonAsync($"{ApiURL}/{drug.Id}", drug);
		}

		public async Task<IEnumerable<Drug>> GetAllDrugs()
		{
			var result = await httpClient.GetStringAsync(ApiURL);

			return JsonConvert.DeserializeObject<IEnumerable<Drug>>(result);
		}

		public async Task<IEnumerable<Drug>> GetClinicDrugs(Guid drugId)

		{
			var apiUrl = $"{ApiURL}/clinic/{drugId}";

			var result = await httpClient.GetStringAsync(apiUrl);

			return JsonConvert.DeserializeObject<IEnumerable<Drug>>(result);
		}

		public async Task DeleteDrug(Guid drugId)
		{
			await httpClient.DeleteAsync($"{ApiURL}/{drugId}");
		}

	}
}
