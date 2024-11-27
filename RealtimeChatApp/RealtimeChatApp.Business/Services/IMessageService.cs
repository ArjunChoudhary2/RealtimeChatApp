using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public interface IMessageService
    {
        Task<Messages> GetMessageByIdAsync(int messageId);
        Task<IEnumerable<Messages>> GetAllMessagesAsync();
        Task SendMessageAsync(Messages message);
        Task UpdateMessageAsync(Messages message);
        Task DeleteMessageAsync(int messageId);
    }

}
