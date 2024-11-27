using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IChatRepository : IBaseRepository<Chats>
    {
        Task<Chats> GetByIdAsync(Guid id);
        Task<IEnumerable<Chats>> GetAllAsync(Expression<Func<Chats, bool>>? filter = null, string? includeProperties = null);
        Task AddAsync(Chats chat);
        Task UpdateAsync(Chats chat);
        Task DeleteAsync(Guid id);
    }
}
