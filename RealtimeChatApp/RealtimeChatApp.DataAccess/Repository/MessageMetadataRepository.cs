using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class MessageMetadataRepository : BaseRepository<Users>, IMessageMetadataRepository

{

        private readonly ChatAppDbContext _context;

        public MessageMetadataRepository(ChatAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MessageMetadata> GetByIdAsync(int id) => await _context.MessageMetadata.FindAsync(id);

        public async Task<IEnumerable<MessageMetadata>> GetAllAsync() => await _context.MessageMetadata.ToListAsync();

        public async Task AddAsync(MessageMetadata metadata)
        {
            _context.MessageMetadata.Add(metadata);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MessageMetadata metadata)
        {
            _context.MessageMetadata.Update(metadata);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var metadata = await _context.MessageMetadata.FindAsync(id);
            if (metadata != null)
            {
                _context.MessageMetadata.Remove(metadata);
                await _context.SaveChangesAsync();
            }
        }
    }

}
