using Microsoft.AspNetCore.Components;
using System;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.Clinics
{
    public partial class IndexPageBase : ComponentBase
    {
        [Inject]
        private IClinicService ClinicService { get; set; }

        protected List<Clinic>? Clinics { get; set; } = null;

        protected bool ShowClinicForm { get; set; } = false;

        protected Clinic Clinic { get; set; } = new Clinic();

        protected bool IsNewEntity { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            await ReadClinicsAsync();
		}

        protected void OnAddButtonClick()
        {
            Clinic = new Clinic();
			IsNewEntity = true;
			ShowClinicForm = true;
		}

		protected void OnEditButtonClick(Clinic editClinic)
		{
			Clinic = new Clinic
			{
				Id = editClinic.Id,
				Name = editClinic.Name,
				Address = editClinic.Address,
				Email = editClinic.Email,
				WebsiteUrl = editClinic.WebsiteUrl
			};
			IsNewEntity = false;
			ShowClinicForm = true;
		}

		protected void OnCancelButtonClick()
        {
			ShowClinicForm = false;
		}

		protected async Task OnValidSubmitAsync()
        {
			if (IsNewEntity)
			{
				await ClinicService.InsertClinic(Clinic);
			}
			else
			{
				await ClinicService.UpdateClinic(Clinic);
			}

			ShowClinicForm = false;

			await ReadClinicsAsync();
		}

		protected async Task OnDeleteAsync(Clinic deleteClinic)
		{
			await ClinicService.DeleteClinic(deleteClinic.Id);

			await ReadClinicsAsync();
		}


		private async Task ReadClinicsAsync()
        {
			Clinics = (await ClinicService.GetAllClinics()).ToList();
		}
	}
}
