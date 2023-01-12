using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using VetExpert.Domain;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClinicUser.Appointments
{
	public partial class IndexPageBase : ComponentBase
	{
		[Inject]
		private IAppointmentService AppointmentService { get; set; } = default!;

        [Inject]
        private IClinicService ClinicService { get; set; } = default!;

        [Inject]
        private IBillService BillService { get; set; } = default!;

		[Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        protected Guid CurrentClinicId { get; set; } = Guid.Empty;

        protected IList<Appointment>? Appointments { get; set; } = null;

        protected bool ShowInfoPopup { get; set; } = false;

        protected Appointment SelectedAppointment { get; set; } = new Appointment();

        protected bool ShowBillForm { get; set; } = false;

        protected Bill Bill { get; set; } = new Bill();

        protected string BillingClientName { get; set; } = string.Empty;

		protected async override Task OnInitializedAsync()
		{
			await GetCurrentClinicId();
			await ReadAppointmentsAsync();
		}

		protected async Task OnAcceptAppointmentClick(Appointment appointment)
		{
			appointment.IsConfirmed = true;

			await AppointmentService.UpdateAppointment(appointment);

			await ReadAppointmentsAsync();
		}

		protected async Task OnDeclineAppointmentClick(Appointment appointment)
		{
			await AppointmentService.DeleteAppointment(appointment.Id);

			await ReadAppointmentsAsync();
		}

		protected void OnInfoPopupClick(Appointment appointment)
		{
			SelectedAppointment = appointment;

			ShowInfoPopup = true;
		}

		protected void OnInfoPopupClose()
		{
			ShowInfoPopup = false;
		}

		protected void OnAddButtonClick(Appointment appointment)
		{
			Bill = new Bill
			{
				UserId = appointment.UserId,
				ClinicId = appointment.ClinicId,
				DateTime = DateTime.Now
			};

			BillingClientName = appointment.User.Name;

			ShowBillForm = true;
		}

		protected void OnCancelButtonClick()
		{
			ShowBillForm = false;
		}

		protected async Task OnBillValidSubmitAsync()
		{
			await BillService.InsertBill(Bill);

			ShowBillForm = false;
		}

		private async Task GetCurrentClinicId()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var applicationUserId = authState.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => new Guid(x.Value)).FirstOrDefault();

			var clinic = await ClinicService.GetByAppUserId(applicationUserId);
			CurrentClinicId = clinic.Id;
		}

		private async Task ReadAppointmentsAsync()
		{
			Appointments = (await AppointmentService.GetClinicAppointments(CurrentClinicId)).ToList();
		}
	}
}
