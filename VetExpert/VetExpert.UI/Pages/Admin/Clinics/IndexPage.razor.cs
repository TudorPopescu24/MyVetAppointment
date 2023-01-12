using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using VetExpert.Domain;
using VetExpert.Domain.Dto;
using VetExpert.UI.Dto;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.Admin.Clinics
{
    public partial class IndexPageBase : ComponentBase
    {
        [Inject]
        private IClinicService ClinicService { get; set; } = default!;

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; } = default!;

        [Inject]
        private IMapper Mapper { get; set; } = default!;

        protected List<Clinic>? Clinics { get; set; } = null;

        protected Guid DeleteClinicId { get; set; } = Guid.Empty;

        protected bool ShowClinicForm { get; set; } = false;

        protected CreateClinicDto Clinic { get; set; } = new CreateClinicDto();

		protected bool IsNewEntity { get; set; } = false;

        public bool ShowDeleteConfirmationPop { get; set; }
        [Parameter] public string Message { get; set; } = "Are you sure?";
        [Parameter] public EventCallback<bool> ConfirmedChanged { get; set; } 

        public async Task DeleteConfirmation(bool value)
        {
            ShowDeleteConfirmationPop = false;
            await ConfirmedChanged.InvokeAsync(value);
        }

        public void ShowDeleteConfirmation(Clinic clinic)
        {
            DeleteClinicId = clinic.Id;
            ShowDeleteConfirmationPop = true;
        }

        protected async override Task OnInitializedAsync()
        {
            await ReadClinicsAsync();
		}

        protected void OnAddButtonClick()
        {
            Clinic = new CreateClinicDto();
			IsNewEntity = true;
			ShowClinicForm = true;
		}

		protected void OnEditButtonClick(Clinic editClinic)
		{
			Clinic = new CreateClinicDto
			{
				Id = editClinic.Id,
				Name = editClinic.Name,
				Address = editClinic.Address,
				Email = editClinic.Email,
				WebsiteUrl = editClinic.WebsiteUrl
			};
            Clinic.UserName = Clinic.Password = Clinic.ConfirmPassword = "Password"; //no validation error
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
                var appUserId = await CreateApplicationUser();
                var clinic = Mapper.Map<Clinic>(Clinic);
                clinic.ApplicationUserId = appUserId;
                await ClinicService.InsertClinic(clinic);
			}
			else
			{
                var clinic = Mapper.Map<Clinic>(Clinic);
                await ClinicService.UpdateClinic(clinic);
			}

			ShowClinicForm = false;

			await ReadClinicsAsync();
		}



        protected async Task OnDeleteAsync()
        {
            await ClinicService.DeleteClinic(DeleteClinicId);
            ShowDeleteConfirmationPop = false;
            await ReadClinicsAsync();
        }

        private async Task ReadClinicsAsync()
        {
			Clinics = (await ClinicService.GetAllClinics()).ToList();
		}

        private async Task<Guid> CreateApplicationUser()
        {
            UserLoginDto userLoginDto = new UserLoginDto
            {
                Username = Clinic.UserName,
                Password = Clinic.Password
            };

            ApplicationUser appUser = await AuthenticationService.RegisterClinic(userLoginDto);

            return appUser.Id;
        }
    }
}
