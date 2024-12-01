using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System.Linq.Expressions;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class MessageRepository : BaseRepository<Messages>,IMessageRepository
{
        private readonly ChatAppDbContext _context;

        public MessageRepository(ChatAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Messages>> GetChatHistoryAsync(Guid user1, Guid user2)
        {
            return await _context.Messages
                .Where(m =>
                    (m.SenderId == user1 && m.ReceiverId == user2) ||
                    (m.SenderId == user2 && m.ReceiverId == user1))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<Messages> GetByIdAsync(int id) => await _context.Messages.FindAsync(id);

        public async Task<IEnumerable<Messages>> GetAllAsync() => await _context.Messages.ToListAsync();

        public async Task AddAsync(Messages message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Messages message)
        {
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }

    }

}
