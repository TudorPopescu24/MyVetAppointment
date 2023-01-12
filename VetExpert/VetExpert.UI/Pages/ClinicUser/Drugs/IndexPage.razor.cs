using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClinicUser.Drugs
{
	public partial class IndexPageBase : ComponentBase
    {

		[Inject]
		private IDrugService DrugService { get; set; }

		[Inject]
		private IClinicService ClinicService { get; set; } = default!;

		[Inject]
		private IMapper Mapper { get; set; } = default!;

		[Inject]
		private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

		protected Guid CurrentClinicId { get; set; } = Guid.Empty;

		protected List<Drug>? Drugs { get; set; } = null;

		protected Drug Drug { get; set; } = new Drug();


		protected bool IsNewEntity { get; set; } = false;

		protected bool ShowDrugForm { get; set; } = false;

		protected async override Task OnInitializedAsync()
		{
			await GetCurrentClinicId();
			await ReadDrugsAsync();
		}

		protected void OnAddButtonClick()
		{
			Drug = new Drug
			{
				ClinicId = CurrentClinicId
			};
			IsNewEntity = true;
			ShowDrugForm = true;
		}


		protected void OnEditButtonClick(Drug editDrug)
		{
			Drug = new Drug
			{
				Id = editDrug.Id,
				Name = editDrug.Name,
				Manufacturer = editDrug.Manufacturer,
				Weight = editDrug.Weight,
				Prospect= editDrug.Prospect,
				Price= editDrug.Price,
				ClinicId = CurrentClinicId
			};
			IsNewEntity = false;
			ShowDrugForm = true;
		}

		protected async Task OnValidSubmitAsync()
		{
			if (IsNewEntity)
			{

				var drug = Mapper.Map<Drug>(Drug);
				await DrugService.InsertDrug(drug);
			}
			else
			{
				var drug = Mapper.Map<Drug>(Drug);
				await DrugService.UpdateDrug(drug);
			}

			ShowDrugForm = false;

			await ReadDrugsAsync();
		}


		protected async Task OnDeleteAsync(Drug deleteDrug)
		{
			await DrugService.DeleteDrug(deleteDrug.Id);

			await ReadDrugsAsync();
		}

		protected void OnCancelButtonClick()
		{
			ShowDrugForm = false;
		}

		private async Task GetCurrentClinicId()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var applicationUserId = authState.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => new Guid(x.Value)).FirstOrDefault();

			var clinic = await ClinicService.GetByAppUserId(applicationUserId);
			CurrentClinicId = clinic.Id;
		}

		private async Task ReadDrugsAsync()
		{
			 Drugs = (await DrugService.GetClinicDrugs(CurrentClinicId)).ToList();
		}
	}
}
