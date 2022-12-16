using System.Net.Http.Json;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
    public class DoctorService : IDoctorService
	{
		private const string ApiURL = "https://localhost:7231/api/Doctors";
		private readonly HttpClient httpClient;

		public DoctorService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task InsertDoctor(Doctor doctor)
		{
			var response = await httpClient.PostAsJsonAsync(ApiURL, doctor);
		}
	}
}
