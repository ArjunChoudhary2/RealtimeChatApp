using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IMessageRepository : IBaseRepository<Messages>
    {
        Task<Messages> GetByIdAsync(int id);
        Task<IEnumerable<Messages>> GetAllAsync();
        Task AddAsync(Messages message);
        Task UpdateAsync(Messages message);
        Task DeleteAsync(int id);
    }
}
