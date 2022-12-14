using Microsoft.EntityFrameworkCore;
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

        public async Task Add(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity?> Get(Guid id)
        {
            return await context.FindAsync<TEntity>(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await context
                .Set<TEntity>()
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>()
                .AsQueryable()
                .Where(predicate).ToListAsync();
        }

		public void Update(TEntity entity)
		{
			context.Update(entity);
		}

		public void SaveChanges()
        {
            context.SaveChanges();
        }

		public async Task SaveChangesAsync()
		{
			await context.SaveChangesAsync();
		}

	}
}
