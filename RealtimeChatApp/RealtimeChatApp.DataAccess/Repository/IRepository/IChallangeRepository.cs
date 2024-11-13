using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IChallangeRepository : IBaseRepository<Challenges>
    {
        Task<Challenges> GetByIdAsync(int id);
        Task<IEnumerable<Challenges>> GetAllAsync();
        Task AddAsync(Challenges challenge);
        Task UpdateAsync(Challenges challenge);
        Task DeleteAsync(int id);
    }
}
