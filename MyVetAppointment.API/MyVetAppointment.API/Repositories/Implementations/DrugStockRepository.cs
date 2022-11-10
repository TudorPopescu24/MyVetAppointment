using MyVetAppointment.API.Data;
using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;


namespace MyVetAppointment.API.Repositories.Implementations
{
    public class DrugStockRepository : IRepository<DrugStock>
    {
        private readonly MainDbContext _context;


        public DrugStockRepository(MainDbContext context)
        {
            _context = context;
        }

        public void Add(DrugStock drugStock)
        {
            _context.DrugStocks.Add(drugStock);
            _context.SaveChanges();
        }

        public void Update(DrugStock drugStock)
        {
            _context.DrugStocks.Update(drugStock);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var drugStock = _context.DrugStocks.FirstOrDefault(d => d.Id == id);

            if (drugStock != null)
            {
                _context.DrugStocks.Remove(drugStock);
                _context.SaveChanges();
            }
        }
    }
}