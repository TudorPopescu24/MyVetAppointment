using System.Linq.Expressions;

namespace VetExpert.Infrastructure
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        TEntity? Get(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void SaveChanges();
    }
}
