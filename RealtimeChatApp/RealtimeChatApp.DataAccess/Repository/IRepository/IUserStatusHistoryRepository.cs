using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IUserStatusHistoryRepository : IBaseRepository<UserStatusHistory>
    {
        Task<UserStatusHistory> GetByIdAsync(int id);
        Task<IEnumerable<UserStatusHistory>> GetAllAsync();
        Task AddAsync(UserStatusHistory userStatusHistory);
        Task UpdateAsync(UserStatusHistory userStatusHistory);
        Task DeleteAsync(int id);
    }
}
