using Microsoft.AspNetCore.Components;
using System;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClinicUser.Doctors
{
    public partial class IndexPageBase : ComponentBase
    {

		[Inject]
		private IDoctorService DoctorService { get; set; }
		protected List<Doctor>? Doctors { get; set; } = null;
		protected Doctor Doctor { get; set; } = new Doctor();

		protected bool IsNewEntity { get; set; } = false;

		protected bool ShowDoctorForm { get; set; } = false;



		protected async override Task OnInitializedAsync()
		{
			await ReadDoctorsAsync();
		}

		private async Task ReadDoctorsAsync()
        {
			Doctors = (await DoctorService.GetAllDoctors()).ToList();
		}

		protected void OnEditButtonClick(Doctor editDoctor)
		{
			Doctor = new Doctor
			{
				Id = editDoctor.Id,
				FirstName = editDoctor.FirstName,
				LastName = editDoctor.LastName,
				Email = editDoctor.Email
			};
			IsNewEntity = false;
			ShowDoctorForm = true;
		}

		protected async Task OnValidSubmitAsync()
		{
			
				await DoctorService.UpdateDoctor(Doctor);
			

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
	}
}
