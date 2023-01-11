using Newtonsoft.Json;
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
			await httpClient.PostAsJsonAsync(ApiURL, doctor);
		}

		public async Task UpdateDoctor(Doctor doctor)
		{
			await httpClient.PutAsJsonAsync($"{ApiURL}/{doctor.Id}", doctor);
		}



		public async Task<IEnumerable<Doctor>?> GetAllDoctors()
		{
			var result = await httpClient.GetStringAsync(ApiURL);

			return JsonConvert.DeserializeObject<IEnumerable<Doctor>>(result);
		}

		public async Task DeleteDoctor(Guid doctorId)
		{
			await httpClient.DeleteAsync($"{ApiURL}/{doctorId}");
		}
	}
}
