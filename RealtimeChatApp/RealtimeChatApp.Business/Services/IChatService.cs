using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public interface IChatService
    {
        Task<Chats> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chats>> GetAllChatsAsync();
        Task CreateChatAsync(Chats chat);
        Task UpdateChatAsync(Chats chat);
        Task DeleteChatAsync(int chatId);
    }

}
