using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        protected Guid CurrentUserId { get; set; } = Guid.Empty;

        protected IList<Appointment>? Appointments { get; set; } = null;

		protected async override Task OnInitializedAsync()
		{
			await ReadAppointmentsAsync();
		}

		private async Task ReadAppointmentsAsync()
		{
			Appointments = (await AppointmentService.GetAllAppointments()).ToList();
		}
	}
}
