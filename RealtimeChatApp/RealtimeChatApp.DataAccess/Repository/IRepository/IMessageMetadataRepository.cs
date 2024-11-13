using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IMessageMetadataRepository : IBaseRepository<MessageMetadata>
    {
        Task<MessageMetadata> GetByIdAsync(int id);
        Task<IEnumerable<MessageMetadata>> GetAllAsync();
        Task AddAsync(MessageMetadata metadata);
        Task UpdateAsync(MessageMetadata metadata);
        Task DeleteAsync(int id);
    }
}
