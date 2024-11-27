using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Chats> GetChatByIdAsync(int chatId)
        {
            return await _chatRepository.GetByIdAsync(chatId);
        }

        public async Task<IEnumerable<Chats>> GetAllChatsAsync()
        {
            return await _chatRepository.GetAllAsync();
        }

        public async Task CreateChatAsync(Chats chat)
        {
            await _chatRepository.AddAsync(chat);
        }

        public async Task UpdateChatAsync(Chats chat)
        {
            await _chatRepository.UpdateAsync(chat);
        }

        public async Task DeleteChatAsync(int chatId)
        {
            await _chatRepository.DeleteAsync(chatId);
        }
    }

}
