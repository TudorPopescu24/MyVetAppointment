using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
    public class BillService : IBillService
	{
		private const string ApiURL = "https://localhost:7231/api/Bills";
		private readonly HttpClient httpClient;

		public BillService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task InsertBill(Bill bill)
		{
			await httpClient.PostAsJsonAsync(ApiURL, bill);
		}

		public async Task UpdateBill(Bill bill)
		{
			await httpClient.PutAsJsonAsync($"{ApiURL}/{bill.Id}", bill);
		}

		public async Task<IEnumerable<Bill>> GetAllBills()
		{
			var result = await httpClient.GetStringAsync(ApiURL);

			return JsonConvert.DeserializeObject<IEnumerable<Bill>>(result);
		}

		public async Task<IEnumerable<Bill>> GetClinicBills(Guid clinicId)
		{
			var apiUrl = $"{ApiURL}/clinic/{clinicId}";

			var result = await httpClient.GetStringAsync(apiUrl);

			return JsonConvert.DeserializeObject<IEnumerable<Bill>>(result);
		}

		public async Task<IEnumerable<Bill>> GetClientBills(Guid userId)
		{
			var apiUrl = $"{ApiURL}/client/{userId}";

			var result = await httpClient.GetStringAsync(apiUrl);

			return JsonConvert.DeserializeObject<IEnumerable<Bill>>(result);
		}

		public async Task DeleteBill(Guid billId)
		{
			await httpClient.DeleteAsync($"{ApiURL}/{billId}");
		}
	}
}
