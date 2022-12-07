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

        public List<Clinic>? Clinics { get; set; } = null;
        protected async override Task OnInitializedAsync()
        {
            Clinics = (await ClinicService.GetAllClinics()).ToList();
        }
    }
}
