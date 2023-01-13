using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
    public class DrugStockService : IDrugStockService
	{
		private const string ApiURL = "https://localhost:7231/api/DrugStocks";
		private readonly HttpClient httpClient;

		public DrugStockService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task InsertDrugStock(DrugStock drugStock)
		{
			await httpClient.PostAsJsonAsync(ApiURL, drugStock);
		}

		public async Task UpdateDrugStock(DrugStock drugStock)
		{
			await httpClient.PutAsJsonAsync($"{ApiURL}/{drugStock.Id}", drugStock);
		}

		public async Task<IEnumerable<DrugStock>> GetAllDrugStocks()
		{
			var result = await httpClient.GetStringAsync(ApiURL);

			return JsonConvert.DeserializeObject<IEnumerable<DrugStock>>(result);
		}

		public async Task<IEnumerable<DrugStock>> GetDrugStocks(Guid drugId)

		{
			var apiUrl = $"{ApiURL}/drug/{drugId}";

			var result = await httpClient.GetStringAsync(apiUrl);

			return JsonConvert.DeserializeObject<IEnumerable<DrugStock>>(result);
		}

		public async Task DeleteDrugStock(Guid drugStockId)
		{
			await httpClient.DeleteAsync($"{ApiURL}/{drugStockId}");
		}

	}
}
