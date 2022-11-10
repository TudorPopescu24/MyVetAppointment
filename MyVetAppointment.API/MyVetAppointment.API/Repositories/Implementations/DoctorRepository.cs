using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;

namespace MyVetAppointment.API.Repositories.Implementations
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly MainDbContext _context;


        public DoctorRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(c => c.Id == id);

            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
        }
    }
}
