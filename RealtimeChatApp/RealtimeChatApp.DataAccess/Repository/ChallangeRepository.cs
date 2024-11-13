using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class ChallangeRepository : BaseRepository<Challenges>,IChallangeRepository
{
    private readonly ChatAppDbContext _context;

    public ChallangeRepository(ChatAppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Challenges> GetByIdAsync(int id) => await _context.Challenges.FindAsync(id);

    public async Task<IEnumerable<Challenges>> GetAllAsync() => await _context.Challenges.ToListAsync();

    public async Task AddAsync(Challenges challange)
    {
        _context.Challenges.Add(challange);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Challenges challenge)
    {
        _context.Challenges.Update(challenge);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var challange = await _context.Challenges.FindAsync(id);
        if (challange != null)
        {
            _context.Challenges.Remove(challange);
            await _context.SaveChangesAsync();
        }
    }
}

}
