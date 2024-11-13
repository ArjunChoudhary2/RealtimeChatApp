using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class UserStatusRepository : BaseRepository<Chats>,IUserStatusHistoryRepository
{
        private readonly ChatAppDbContext _context;

        public UserStatusRepository(ChatAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserStatusHistory> GetByIdAsync(int id) => await _context.UserStatusHistories.FindAsync(id);

        public async Task<IEnumerable<UserStatusHistory>> GetAllAsync() => await _context.UserStatusHistories.ToListAsync();

        public async Task AddAsync(UserStatusHistory userStatusHistory)
        {
            _context.UserStatusHistories.Add(userStatusHistory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserStatusHistory userStatusHistory)
        {
            _context.UserStatusHistories.Update(userStatusHistory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userStatusHistory = await _context.UserStatusHistories.FindAsync(id);
            if (userStatusHistory != null)
            {
                _context.UserStatusHistories.Remove(userStatusHistory);
                await _context.SaveChangesAsync();
            }
        }

    }

}
