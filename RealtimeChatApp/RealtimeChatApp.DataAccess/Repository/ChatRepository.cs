using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class ChatRepository : BaseRepository<Chats>,IChatRepository
{
        private readonly ChatAppDbContext _context;

        public ChatRepository(ChatAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Chats> GetByIdAsync(Guid id) => await _context.Chats.FindAsync(id);

        public async Task<IEnumerable<Chats>> GetAllAsync(Expression<Func<Chats, bool>>? filter, string? includeProperties = null) {
            IQueryable<Chats> query = _context.Chats;
            if (filter != null) query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync(); }

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

        public async Task DeleteAsync(Guid id)
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
