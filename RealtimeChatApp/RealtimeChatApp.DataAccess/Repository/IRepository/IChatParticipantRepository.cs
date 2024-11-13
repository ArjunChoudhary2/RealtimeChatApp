using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IChatParticipantRepository : IBaseRepository<ChatParticipants>
    {
        Task<ChatParticipants> GetByIdAsync(int id);
        Task<IEnumerable<ChatParticipants>> GetAllAsync();
        Task AddAsync(ChatParticipants chatParticipant);
        Task UpdateAsync(ChatParticipants chatParticipant);
        Task DeleteAsync(int id);
    }
}
