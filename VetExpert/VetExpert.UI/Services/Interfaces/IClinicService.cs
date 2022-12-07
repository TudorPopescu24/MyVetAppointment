using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces
{
    public interface IClinicService
    {
        Task<IEnumerable<Clinic>> GetAllClinics();
    }
}
