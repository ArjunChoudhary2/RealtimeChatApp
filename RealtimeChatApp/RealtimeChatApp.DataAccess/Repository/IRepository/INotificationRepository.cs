using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface INotificationRepository : IBaseRepository<Notifications>
    {
        Task<Notifications> GetByIdAsync(int id);
        Task<IEnumerable<Notifications>> GetAllAsync();
        Task AddAsync(Notifications notification);
        Task UpdateAsync(Notifications notification);
        Task DeleteAsync(int id);
    }
}
