using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
	public class ClinicService : IClinicService
    {
        private const string ApiURL = "https://localhost:7231/api/Clinics";
        private readonly HttpClient httpClient;

        public ClinicService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Clinic>?> GetAllClinics()
        {
            var result = await httpClient.GetStringAsync(ApiURL);

            return JsonConvert.DeserializeObject<IEnumerable<Clinic>>(result);
        }
		public async Task<Clinic> GetByAppUserId(Guid appUserId)
		{
			var apiUrl = $"{ApiURL}/applicationUser/{appUserId}";

			var result = await httpClient.GetStringAsync(apiUrl);

			return JsonConvert.DeserializeObject<Clinic?>(result);
		}
		public async Task InsertClinic(Clinic clinic)
		{
			await httpClient.PostAsJsonAsync(ApiURL, clinic);
		}

		public async Task UpdateClinic(Clinic clinic)
		{
			await httpClient.PutAsJsonAsync($"{ApiURL}/{clinic.Id}", clinic);
		}

		public async Task DeleteClinic(Guid clinicId)
		{
			await httpClient.DeleteAsync($"{ApiURL}/{clinicId}");
		}

		

	}
}
