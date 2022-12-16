using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces
{
    public interface IDoctorService
    {
        Task InsertDoctor(Doctor doctor);
    }
}