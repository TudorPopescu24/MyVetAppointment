using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces
{
    public interface IClinicService
    {
        Task<IEnumerable<Clinic>?> GetAllClinics();
		Task<Clinic> GetByAppUserId(Guid appUserId);
		Task InsertClinic(Clinic clinic);
        Task UpdateClinic(Clinic clinic);
        Task DeleteClinic(Guid clinicId);
	}
}
