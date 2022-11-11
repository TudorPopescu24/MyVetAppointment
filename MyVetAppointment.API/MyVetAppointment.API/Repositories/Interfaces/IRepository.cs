namespace MyVetAppointment.API.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);

        void Delete(int id);

        void Update(TEntity entity);
    }
}
