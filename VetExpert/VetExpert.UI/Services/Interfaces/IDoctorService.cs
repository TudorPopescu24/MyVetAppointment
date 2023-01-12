using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces
{
    public interface IDoctorService
    {
		Task<IEnumerable<Doctor>> GetAllDoctors();
		Task InsertDoctor(Doctor doctor);

		Task<IEnumerable<Doctor>> GetClinicDoctors(Guid clinicId);
		Task UpdateDoctor(Doctor doctor);
		Task DeleteDoctor(Guid doctorId);
	}
}