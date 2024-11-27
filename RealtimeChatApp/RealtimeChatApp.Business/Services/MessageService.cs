using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Messages> GetMessageByIdAsync(int messageId)
        {
            return await _messageRepository.GetByIdAsync(messageId);
        }

        public async Task<IEnumerable<Messages>> GetAllMessagesAsync()
        {
            return await _messageRepository.GetAllAsync();
        }

        public async Task SendMessageAsync(Messages message)
        {
            await _messageRepository.AddAsync(message);
        }

        public async Task UpdateMessageAsync(Messages message)
        {
            await _messageRepository.UpdateAsync(message);
        }

        public async Task DeleteMessageAsync(int messageId)
        {
            await _messageRepository.DeleteAsync(messageId);
        }
    }

}
