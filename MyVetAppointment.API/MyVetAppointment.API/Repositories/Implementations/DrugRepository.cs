using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;

namespace MyVetAppointment.API.Repositories.Implementations
{
    public class DrugRepository : IRepository<Drug>
    {
        private readonly MainDbContext _context;

        public DrugRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(Drug drug)
        {
            _context.Drugs.Add(drug);
            _context.SaveChanges();
        }

        public void Update(Drug drug)
        {
            _context.Drugs.Update(drug);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var drug = _context.Drugs.FirstOrDefault(d => d.Id == id);

            if (drug != null)
            {
                _context.Drugs.Remove(drug);
                _context.SaveChanges();
            }
        }
    }
}
