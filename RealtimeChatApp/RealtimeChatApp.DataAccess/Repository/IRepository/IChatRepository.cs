using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IChatRepository : IBaseRepository<Chats>
    {
        Task<Chats> GetByIdAsync(int id);
        Task<IEnumerable<Chats>> GetAllAsync();
        Task AddAsync(Chats chat);
        Task UpdateAsync(Chats chat);
        Task DeleteAsync(int id);
    }
}
