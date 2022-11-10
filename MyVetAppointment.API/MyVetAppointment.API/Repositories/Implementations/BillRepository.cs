using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;

namespace MyVetAppointment.API.Repositories.Implementations
{
    public class BillRepository : IRepository<Bill>
    {
        private readonly MainDbContext _context;


        public BillRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(Bill bill)
        {
            _context.Bills.Add(bill);
            _context.SaveChanges();
        }

        public void Update(Bill bill)
        {
            _context.Bills.Update(bill);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var bill = _context.Bills.FirstOrDefault(b => b.Id == id);

            if (bill != null)
            {
                _context.Bills.Remove(bill);
                _context.SaveChanges();
            }
        }
    }
}