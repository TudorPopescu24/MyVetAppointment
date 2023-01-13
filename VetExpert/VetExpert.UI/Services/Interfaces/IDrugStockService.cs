using VetExpert.Domain;

namespace VetExpert.UI.Services.Implementations;

public interface IDrugStockService
{
	Task InsertDrugStock(DrugStock drugStock);
	Task UpdateDrugStock(DrugStock drugStock);
	Task<IEnumerable<DrugStock>> GetAllDrugStocks();
	Task<IEnumerable<DrugStock>> GetDrugStocks(Guid drugId);
	Task DeleteDrugStock(Guid drugStockId);
}