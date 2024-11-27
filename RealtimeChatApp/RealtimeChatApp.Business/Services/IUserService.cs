using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public interface IUserService
    {
        Users GetUsersById(Guid userId);
        Task<Users> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task AddUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(Guid userId);
    }
}
