using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;

namespace MyVetAppointment.API.Repositories.Implementations
{
    public class PetRepository : IRepository<Pet>
    {
        private readonly MainDbContext _context;


        public PetRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(Pet pet)
        {
            _context.Pets.Add(pet);
            _context.SaveChanges();
        }

        public void Update(Pet pet)
        {
            _context.Pets.Update(pet);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var pet = _context.Pets.FirstOrDefault(c => c.Id == id);

            if (pet != null)
            {
                _context.Pets.Remove(pet);
                _context.SaveChanges();
            }
        }
    }
}
