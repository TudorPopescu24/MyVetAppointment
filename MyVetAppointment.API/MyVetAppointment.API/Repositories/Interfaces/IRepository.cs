namespace MyVetAppointment.API.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity category);

        void Delete(int id);

        void Update(TEntity category);
    }
}
