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

		public async Task<IEnumerable<Appointment>> GetUserAppointments(Guid userId)
		{
			string apiUrl = $"{ApiURL}/user/{userId}";

			var result = await httpClient.GetStringAsync(apiUrl);

			return JsonConvert.DeserializeObject<IEnumerable<Appointment>>(result);
		}

		public async Task<bool> InsertAppointment(Appointment appointment)
		{
			var result = await httpClient.PostAsJsonAsync(ApiURL, appointment);

			return result.IsSuccessStatusCode;
		}

		public async Task UpdateAppointment(Appointment appointment)
		{
			await httpClient.PutAsJsonAsync($"{ApiURL}/{appointment.Id}", appointment);
		}

		public async Task DeleteAppointment(Guid appointmentId)
		{
			await httpClient.DeleteAsync($"{ApiURL}/{appointmentId}");
		}
	}
}
