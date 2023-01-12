using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using VetExpert.Domain;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClinicUser.Bills
{
	public partial class IndexPageBase : ComponentBase
    {

		[Inject]
		private IBillService BillService { get; set; } = default!;

		[Inject]
		private IClinicService ClinicService { get; set; } = default!;

		[Inject]
		private IMapper Mapper { get; set; } = default!;

		[Inject]
		private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

		protected Guid CurrentClinicId { get; set; } = Guid.Empty;

		protected List<Bill>? Bills { get; set; } = null;

		protected async override Task OnInitializedAsync()
		{
			await GetCurrentClinicId();
			await ReadBillsAsync();
		}

		private async Task GetCurrentClinicId()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			var applicationUserId = authState.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => new Guid(x.Value)).FirstOrDefault();

			var clinic = await ClinicService.GetByAppUserId(applicationUserId);
			CurrentClinicId = clinic.Id;
		}

		private async Task ReadBillsAsync()
		{
			 Bills = (await BillService.GetClinicBills(CurrentClinicId)).ToList();
		}
	}
}
