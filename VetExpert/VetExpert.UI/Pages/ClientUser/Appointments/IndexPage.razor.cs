using Microsoft.AspNetCore.Components;
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
		private IPetService PetService { get; set; } = default!;

		[Inject]
		private IDoctorService DoctorService { get; set; } = default!;

		[Inject]
		private IClinicService ClinicService { get; set; } = default!;

		[Inject]
		private IAuthenticationService AuthenticationService { get; set; } = default!;

		protected IList<Appointment>? Appointments { get; set; } = null;

		protected IList<Doctor>? Doctors { get; set; } = null;

		protected IList<Pet>? Pets { get; set; } = null;

		protected IList<Clinic>? Clinics { get; set; } = null;

		protected bool ShowPetForm { get; set; } = false;

		protected Appointment Appointment { get; set; } = new Appointment();

		protected bool IsNewEntity { get; set; } = false;

		protected async override Task OnInitializedAsync()
		{
			await ReadDoctorsAsync();
			await ReadPetsAsync();
			await ReadClinicsAsync();
			await ReadAppointmentsAsync();
		}

		private async Task ReadAppointmentsAsync()
		{
			Appointments = (await AppointmentService.GetAllAppointments()).ToList();
		}

		private async Task ReadDoctorsAsync()
		{
			Doctors = (await DoctorService.GetAllDoctors()).ToList();
		}

		private async Task ReadPetsAsync()
		{
			Pets = (await PetService.GetAllPets()).ToList();
		}

		private async Task ReadClinicsAsync()
		{
			Clinics = (await ClinicService.GetAllClinics()).ToList();
		}
	}
}
