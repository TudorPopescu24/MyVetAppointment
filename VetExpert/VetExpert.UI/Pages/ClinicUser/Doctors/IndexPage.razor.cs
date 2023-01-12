using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClinicUser.Doctors
{
	public partial class IndexPageBase : ComponentBase
    {

		[Inject]
		private IDoctorService DoctorService { get; set; }

		[Inject]
		private IClinicService ClinicService { get; set; } = default!;

		[Inject]
		private IMapper Mapper { get; set; } = default!;

		[Inject]
		private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

		protected Guid CurrentClinicId { get; set; } = Guid.Empty;

		protected List<Doctor>? Doctors { get; set; } = null;

		protected Doctor Doctor { get; set; } = new Doctor();


		protected bool IsNewEntity { get; set; } = false;

		protected bool ShowDoctorForm { get; set; } = false;

		protected async override Task OnInitializedAsync()
		{
			await GetCurrentClinicId();
			await ReadDoctorsAsync();
		}

		protected void OnAddButtonClick()
		{
			Doctor = new Doctor
			{
				ClinicId = CurrentClinicId
			};
			IsNewEntity = true;
			ShowDoctorForm = true;
		}


		protected void OnEditButtonClick(Doctor editDoctor)
		{
			Doctor = new Doctor
			{
				Id = editDoctor.Id,
				FirstName = editDoctor.FirstName,
				LastName = editDoctor.LastName,
				Email = editDoctor.Email,
				ClinicId = CurrentClinicId
			};
			IsNewEntity = false;
			ShowDoctorForm = true;
		}

		protected async Task OnValidSubmitAsync()
		{
			if (IsNewEntity)
			{

				var doctor = Mapper.Map<Doctor>(Doctor);
				await DoctorService.InsertDoctor(doctor);
			}
			else
			{
				var doctor = Mapper.Map<Doctor>(Doctor);
				await DoctorService.UpdateDoctor(doctor);
			}

			ShowDoctorForm = false;

			await ReadDoctorsAsync();
		}


		protected async Task OnDeleteAsync(Doctor deleteDoctor)
		{
			await DoctorService.DeleteDoctor(deleteDoctor.Id);

			await ReadDoctorsAsync();
		}

		protected void OnCancelButtonClick()
		{
			ShowDoctorForm = false;
		}

		private async Task GetCurrentClinicId()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var applicationUserId = authState.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => new Guid(x.Value)).FirstOrDefault();

			var clinic = await ClinicService.GetByAppUserId(applicationUserId);
			CurrentClinicId = clinic.Id;
		}

		private async Task ReadDoctorsAsync()
		{
			 Doctors = (await DoctorService.GetClinicDoctors(CurrentClinicId)).ToList();
		}
	}
}
