using System.Linq.Expressions;

namespace VetExpert.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected MainDbContext context;

        public Repository(MainDbContext context)
        {
            this.context = context;
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public TEntity? Get(Guid id)
        {
            return context.Find<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context
                .Set<TEntity>()
                .ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>()
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
