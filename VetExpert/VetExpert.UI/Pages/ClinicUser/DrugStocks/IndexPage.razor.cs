using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using VetExpert.Domain;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Pages.ClinicUser.DrugStocks
{
	public partial class IndexPageBase : ComponentBase
    {

		[Inject]
		private IDrugService DrugService { get; set; }

		[Inject]
		private IClinicService ClinicService { get; set; } = default!;

		[Inject]
		private IDrugStockService DrugStockService { get; set; } = default!;

		[Inject]
		private IMapper Mapper { get; set; } = default!;

		[Inject]
		private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

		protected Guid CurrentClinicId { get; set; } = Guid.Empty;

		protected Guid DeleteDrugStockId { get; set; } = Guid.Empty;

		protected List<Drug>? Drugs { get; set; } = null;

		protected List<DrugStock>? DrugStocks { get; set; } = null;

		protected DrugStock DrugStock { get; set; } = new DrugStock();


		protected bool IsNewEntity { get; set; } = false;

		protected bool ShowDrugStockForm { get; set; } = false;

		public bool ShowDeleteConfirmationPop { get; set; }

		[Parameter] 
		public EventCallback<bool> ConfirmedChanged { get; set; }

		public async Task DeleteConfirmation(bool value)
		{
			ShowDeleteConfirmationPop = false;
			await ConfirmedChanged.InvokeAsync(value);
		}

		public void ShowDeleteConfirmation(DrugStock drugStock)
		{
			DeleteDrugStockId = drugStock.Id;
			ShowDeleteConfirmationPop = true;
		}

		protected async Task OnDeleteAsync()
		{
			await DrugStockService.DeleteDrugStock(DeleteDrugStockId);
			ShowDeleteConfirmationPop = false;

			await ReadDrugStocksAsync();
		}

		protected async override Task OnInitializedAsync()
		{
			await GetCurrentClinicId();
			await ReadDrugsAsync();
			await ReadDrugStocksAsync();
		}

		protected void OnAddButtonClick()
		{
			DrugStock = new DrugStock
			{
				ExpirationDate = DateTime.Now
			};

			IsNewEntity = true;
			ShowDrugStockForm = true;
		}

		protected void OnEditButtonClick(DrugStock drugStock)
		{
			DrugStock = new DrugStock
			{
				Id = drugStock.Id,
				Quantity = drugStock.Quantity,
				ExpirationDate = drugStock.ExpirationDate,
				DrugId = drugStock.DrugId
			};

			IsNewEntity = false;
			ShowDrugStockForm = true;
		}

		protected async Task OnValidSubmitAsync()
		{
			if (IsNewEntity)
			{
				await DrugStockService.InsertDrugStock(DrugStock);
			}
			else
			{
				await DrugStockService.UpdateDrugStock(DrugStock);
			}

			ShowDrugStockForm = false;

			await ReadDrugStocksAsync();
		}

		protected void OnCancelButtonClick()
		{
			ShowDrugStockForm = false;
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

		private async Task ReadDrugStocksAsync()
		{
			var allDrugStocks = (await DrugStockService.GetAllDrugStocks()).ToList();

			DrugStocks = allDrugStocks.Where(x => Drugs.Select(x => x.Id).ToList().Contains(x.DrugId)).ToList();
		}
	}
}
