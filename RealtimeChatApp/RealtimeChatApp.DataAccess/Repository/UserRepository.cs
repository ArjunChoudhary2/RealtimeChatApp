using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
namespace RealtimeChatApp.RealtimeChatApp.DataAccess.Repository
{
    
public class UserRepository : BaseRepository<Users>,IUserRepository
{
    private readonly ChatAppDbContext _context;

    public UserRepository(ChatAppDbContext context) : base(context)
    {
            _context = context;
    }
    public Users GetById(Guid id)
        {
            return _context.Users.Find(id);
        }

        void Update(Users user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    public async Task<Users> GetByIdAsync(Guid id) => await _context.Users.FindAsync(id);

    public async Task<IEnumerable<Users>> GetAllAsync() => await _context.Users.ToListAsync();

    public async Task AddAsync(Users user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Users user)
    {
            _context.Users.Update(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

}
