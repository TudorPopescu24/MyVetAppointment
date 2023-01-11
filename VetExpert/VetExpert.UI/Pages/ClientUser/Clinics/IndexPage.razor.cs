using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClientUser.Clinics
{
    public partial class IndexPageBase : ComponentBase
    {
        [Inject]
        private IClinicService ClinicService { get; set; } = default!;

        [Inject]
        private IAppointmentService AppointmentService { get; set; } = default!;

		[Inject]
        private IPetService PetService { get; set; } = default!;

        [Inject]
        private IDoctorService DoctorService { get; set; } = default!;

        [Inject]
        private IUserService UserService { get; set; } = default!;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        protected Guid CurrentUserId { get; set; } = Guid.Empty;

        protected List<Clinic>? Clinics { get; set; } = null;

        protected IList<Pet>? Pets { get; set; } = null;

        protected IList<Doctor>? Doctors { get; set; } = null;

        protected Appointment Appointment { get; set; } = new Appointment();

        protected bool ShowAppointmentForm { get; set; } = false;

        protected bool? Success { get; set; } = null;



        protected async override Task OnInitializedAsync()
        {
            await GetCurrentUserId();
            await ReadClinicsAsync();
            await ReadPetsAsync();
        }

        protected async Task OnMakeAppointmentClick(Clinic clinic)
        {
            Appointment = new Appointment
            {
                ClinicId = clinic.Id,
                UserId = CurrentUserId,
                IsConfirmed = false
            };

            ShowAppointmentForm = true;
        }

        protected void OnCancelAppointmentButtonClick()
        {
	        ShowAppointmentForm = false;
        }

        protected async Task OnValidSubmitAsync()
        {
	        Success = await AppointmentService.InsertAppointment(Appointment);

	        ShowAppointmentForm = false;
        }

		protected async Task OnSeeDoctorsClick(Clinic clinic)
        {

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


        private async Task ReadClinicsAsync()
        {
			Clinics = (await ClinicService.GetAllClinics()).ToList();
		}
    }
}
