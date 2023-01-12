using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAllAppointments();
    Task<IEnumerable<Appointment>> GetUserAppointments(Guid userId);
    Task<bool> InsertAppointment(Appointment appointment);
    Task UpdateAppointment(Appointment appointment);
    Task DeleteAppointment(Guid appointmentId);
}