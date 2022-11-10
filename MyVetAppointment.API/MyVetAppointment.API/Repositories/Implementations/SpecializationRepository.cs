using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;

namespace MyVetAppointment.API.Repositories.Implementations
{
    public class SpecializationRepository : IRepository<Specialization>
    {
        private readonly MainDbContext _context;


        public SpecializationRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(Specialization specialization)
        {
            _context.Specializations.Add(specialization);
            _context.SaveChanges();
        }

        public void Update(Specialization specialization)
        {
            _context.Specializations.Update(specialization);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var specialization = _context.Specializations.FirstOrDefault(c => c.Id == id);

            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                _context.SaveChanges();
            }
        }
    }
}
