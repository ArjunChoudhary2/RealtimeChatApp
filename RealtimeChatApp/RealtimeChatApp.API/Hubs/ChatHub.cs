using Microsoft.AspNetCore.SignalR;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System.Text.RegularExpressions;

namespace RealtimeChatApp.RealtimeChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            var currMessage = new Messages
            {
                SenderId = Guid.Parse(senderId),
                ReceiverId = Guid.Parse(receiverId),
                Content = message,
                Timestamp = DateTime.UtcNow,
                IsRead = false,
                IsChallenge = false
            };

            await _messageRepository.AddAsync(currMessage);
            // Notify the receiver
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);

            // Optionally acknowledge sender
            await Clients.Caller.SendAsync("MessageSent", senderId, receiverId, message);
        }
    }
}
