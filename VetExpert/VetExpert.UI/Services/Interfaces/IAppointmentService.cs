using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAllAppointments();
    Task<bool> InsertAppointment(Appointment appointment);
    Task UpdateAppointment(Appointment appointment);
    Task DeleteUser(Guid userId);
}