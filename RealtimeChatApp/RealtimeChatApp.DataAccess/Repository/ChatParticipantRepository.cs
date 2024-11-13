using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class ChatParticipantRepository : BaseRepository<ChatParticipants>, IChatParticipantRepository
    {
        private readonly ChatAppDbContext _context;

        public ChatParticipantRepository(ChatAppDbContext context) :base(context) 
        {
            _context = context;
        }

        public async Task<ChatParticipants> GetByIdAsync(int id) => await _context.ChatParticipants.FindAsync(id);

        public async Task<IEnumerable<ChatParticipants>> GetAllAsync() => await _context.ChatParticipants.ToListAsync();

        public async Task AddAsync(ChatParticipants chatParticipant)
        {
            _context.ChatParticipants.Add(chatParticipant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChatParticipants chatParticipant)
        {
            _context.ChatParticipants.Update(chatParticipant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chatParticipant = await _context.ChatParticipants.FindAsync(id);
            if (chatParticipant != null)
            {
                _context.ChatParticipants.Remove(chatParticipant);
                await _context.SaveChangesAsync();
            }
        }
    }

}
