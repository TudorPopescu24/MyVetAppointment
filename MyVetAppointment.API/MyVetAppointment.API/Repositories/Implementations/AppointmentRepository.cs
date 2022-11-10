using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;

namespace MyVetAppointment.API.Repositories.Implementations
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private readonly MainDbContext _context;


        public AppointmentRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(c => c.Id == id);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
        }
    }
}
