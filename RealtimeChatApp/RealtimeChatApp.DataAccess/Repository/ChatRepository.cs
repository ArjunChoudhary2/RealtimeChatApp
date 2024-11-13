using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class ChatRepository : BaseRepository<Chats>,IChatRepository
{
        private readonly ChatAppDbContext _context;

        public ChatRepository(ChatAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Chats> GetByIdAsync(int id) => await _context.Chats.FindAsync(id);

        public async Task<IEnumerable<Chats>> GetAllAsync() => await _context.Chats.ToListAsync();

        public async Task AddAsync(Chats chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chats chat)
        {
            _context.Chats.Update(chat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat != null)
            {
                _context.Chats.Remove(chat);
                await _context.SaveChangesAsync();
            }
        }

    }

}
