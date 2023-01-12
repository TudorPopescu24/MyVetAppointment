using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using VetExpert.Domain;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClientUser.Appointments
{
	public partial class IndexPageBase : ComponentBase
	{
		[Inject]
		private IAppointmentService AppointmentService { get; set; } = default!;

        [Inject]
        private IUserService UserService { get; set; } = default!;

        [Inject]
        private IPetService PetService { get; set; } = default!;

		[Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        protected Guid CurrentUserId { get; set; } = Guid.Empty;

        protected List<Pet>? Pets { get; set; } = null;

        protected IList<Appointment>? Appointments { get; set; } = null;

        protected Appointment Appointment { get; set; } = new Appointment();

        protected bool ShowAppointmentForm { get; set; } = false;

		protected async override Task OnInitializedAsync()
		{
			await GetCurrentUserId();
			await ReadPetsAsync();
			await ReadAppointmentsAsync();
		}

		protected async Task OnEditAppointmentClick(Appointment appointment)
		{
			Appointment = appointment;

			ShowAppointmentForm = true;
		}

		protected void OnCancelAppointmentButtonClick()
		{
			ShowAppointmentForm = false;
		}

		protected async Task OnValidSubmitAsync()
		{
			await AppointmentService.UpdateAppointment(Appointment);

			ShowAppointmentForm = false;

			await ReadAppointmentsAsync();
		}

		protected async Task OnDeleteAsync(Appointment appointment)
		{
			await AppointmentService.DeleteAppointment(appointment.Id);

			await ReadAppointmentsAsync();
		}

		private async Task GetCurrentUserId()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var applicationUserId = authState.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => new Guid(x.Value)).FirstOrDefault();

			var user = await UserService.GetByAppUserId(applicationUserId);
			CurrentUserId = user.Id;
		}

		private async Task ReadPetsAsync()
		{
			Pets = (await PetService.GetClientPets(CurrentUserId)).ToList();
		}

		private async Task ReadAppointmentsAsync()
		{
			Appointments = (await AppointmentService.GetUserAppointments(CurrentUserId)).ToList();
		}
	}
}
