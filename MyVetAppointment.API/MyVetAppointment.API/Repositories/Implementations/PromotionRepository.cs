using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;

namespace MyVetAppointment.API.Repositories.Implementations
{
    public class PromotionRepository : IRepository<Promotion>
    {
        private readonly MainDbContext _context;


        public PromotionRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            _context.SaveChanges();
        }

        public void Update(Promotion promotion)
        {
            _context.Promotions.Update(promotion);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var promotion = _context.Promotions.FirstOrDefault(c => c.Id == id);

            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                _context.SaveChanges();
            }
        }
    }
}
