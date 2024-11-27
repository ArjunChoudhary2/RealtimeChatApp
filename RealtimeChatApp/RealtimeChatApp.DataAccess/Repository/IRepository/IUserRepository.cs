using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        Users GetById(Guid id);
        void Update(Users user);
        Task<Users> GetByIdAsync(Guid id);
        Task<IEnumerable<Users>> GetAllAsync();
        Task AddAsync(Users user);
        Task UpdateAsync(Users user);
        Task DeleteAsync(Guid id);
        
    }
}
