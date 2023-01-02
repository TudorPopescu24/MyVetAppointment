using System.Linq.Expressions;

namespace VetExpert.Infrastructure
{
    public interface IRepository<TEntity>
    {
        Task Add(TEntity entity);
		void Delete(TEntity entity);
		void Update(TEntity entity);
		Task<TEntity?> Get(Guid id);
		Task<IEnumerable<TEntity>> GetAll();
		Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity?> FindEntity(Expression<Func<TEntity, bool>> predicate);
		void SaveChanges();
		Task SaveChangesAsync();
    }
}
