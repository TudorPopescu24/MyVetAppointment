using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces
{
    public interface IDrugService
    {
		Task<IEnumerable<Drug>> GetAllDrugs();
		Task InsertDrug(Drug drug);

		Task<IEnumerable<Drug>> GetClinicDrugs(Guid clinicId);
		Task UpdateDrug(Drug drug);
		Task DeleteDrug(Guid drugId);
	}
}