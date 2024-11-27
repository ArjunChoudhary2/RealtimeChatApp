using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System.Linq.Expressions;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        void Update(Users user);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);

    }

}
