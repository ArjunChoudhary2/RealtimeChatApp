using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        Task<Users> GetByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllAsync();
        Task AddAsync(Users user);
        Task UpdateAsync(Users user);
        Task DeleteAsync(int id);
    }
}
