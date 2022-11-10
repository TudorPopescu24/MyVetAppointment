using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;

namespace MyVetAppointment.API.Repositories.Implementations
{
    public class ClinicRepository : IRepository<Clinic>
    {
        private readonly MainDbContext _context;


        public ClinicRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();
        }

        public void Update(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var clinic = _context.Clinics.FirstOrDefault(c => c.Id == id);

            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                _context.SaveChanges();
            }
        }
    }
}
