using VetExpert.Domain;

namespace VetExpert.UI.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>?> GetAllUsers();
        Task<User> GetByAppUserId(Guid appUserId);
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(Guid user);
    }
}
