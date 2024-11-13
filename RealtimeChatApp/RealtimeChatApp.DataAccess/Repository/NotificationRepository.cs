using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class NotificationRepository : BaseRepository<Users>,INotificationRepository
{
        private readonly ChatAppDbContext _context;

        public NotificationRepository(ChatAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Notifications> GetByIdAsync(int id) => await _context.Notifications.FindAsync(id);

        public async Task<IEnumerable<Notifications>> GetAllAsync() => await _context.Notifications.ToListAsync();

        public async Task AddAsync(Notifications notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notifications notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }

}
