using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Users> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddUserAsync(Users user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(Users user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await _userRepository.DeleteAsync(userId);
        }

        public Users GetUsersById(Guid userId)
        {
            return _userRepository.GetById(userId);
        }

        public void UpdateUser(Users user)
        {
            _userRepository.Update(user);
        }
    }

}
