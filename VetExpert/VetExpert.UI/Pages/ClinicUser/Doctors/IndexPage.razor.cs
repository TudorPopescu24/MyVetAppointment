using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using VetExpert.Domain;
using VetExpert.Domain.Dto;
using VetExpert.UI.Dto;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClinicUser.Doctors
{
    public partial class IndexPageBase : ComponentBase
    {

		[Inject]
		private IDoctorService DoctorService { get; set; }

		protected List<Doctor>? Doctors { get; set; } = null;
		//protected Doctor Doctor { get; set; } = new Doctor();

		[Inject]
		private IMapper Mapper { get; set; } = default!;

		protected bool IsNewEntity { get; set; } = false;
		protected CreateDoctorDto Doctor { get; set; } = new CreateDoctorDto();

		protected bool ShowDoctorForm { get; set; } = false;
		//protected Clinic Clinic { get; set; } = new Clinic();
		protected CreateClinicDto Clinic { get; set; } = new CreateClinicDto();
		public Guid ClinicId { get; set; } = Guid.Empty;

		protected async override Task OnInitializedAsync()
		{
			await ReadDoctorsAsync();
		}

		protected void OnAddButtonClick()
		{
			//Doctor = new Doctor
			//{
			//	ClinicId = clinic.Id,
			//	Clinic = clinic
			//};

			Doctor = new CreateDoctorDto();
			IsNewEntity = true;
			ShowDoctorForm = true;
		}

		private async Task ReadDoctorsAsync()
        {
			Doctors = (await DoctorService.GetAllDoctors()).ToList();
		}

		protected void OnEditButtonClick(Doctor editDoctor)
		{
			Doctor = new CreateDoctorDto
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
	}
}
