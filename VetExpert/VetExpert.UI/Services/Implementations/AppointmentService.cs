using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Services.Implementations
{
    public class AppointmentService : IAppointmentService
	{
		private const string ApiURL = "https://localhost:7231/api/Appointments";
		private readonly HttpClient httpClient;

		public AppointmentService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<IEnumerable<Appointment>> GetAllAppointments()
		{
			var result = await httpClient.GetStringAsync(ApiURL);

			return JsonConvert.DeserializeObject<IEnumerable<Appointment>>(result);
		}

		public async Task InsertAppointment(Appointment appointment)
		{
			await httpClient.PostAsJsonAsync(ApiURL, appointment);
		}

		public async Task UpdateAppointment(Appointment appointment)
		{
			await httpClient.PutAsJsonAsync($"{ApiURL}/{appointment.Id}", appointment);
		}

		public async Task DeleteUser(Guid userId)
		{
			await httpClient.DeleteAsync($"{ApiURL}/{userId}");
		}
	}
}
