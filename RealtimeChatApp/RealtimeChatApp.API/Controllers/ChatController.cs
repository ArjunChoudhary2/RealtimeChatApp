using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.API.Hubs;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using RealtimeChatApp.RealtimeChatApp.Domain.Enums;
using System;

namespace RealtimeChatApp.RealtimeChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IUnitOfWork unitOfWork, IHubContext<ChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        // Create a new chat
        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] Chats newChat)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid chat data");

            // Ensure user1 and user2 are different
            if (newChat.User1_Id == newChat.User2_Id)
                return BadRequest("A chat must have two different participants");

            // Create chat object
            var chat = new Chats
            {
                Id = Guid.NewGuid(),
                User1_Id = newChat.User1_Id,
                User2_Id = newChat.User2_Id,
                ChatName = newChat.ChatName?.Trim(),
                CreatedAt = DateTime.UtcNow,
                Messages = newChat.Messages ?? new List<Messages>() // Use empty list if Messages is null
            };

            await _unitOfWork.Chats.AddAsync(chat);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetChatById), new { id = chat.Id }, chat);
        }



        // Get all chats for a specific user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllChatsForUser(Guid userId)
        {
            var chats = await _unitOfWork.Chats.GetAllAsync(
                c => c.User1_Id == userId || c.User2_Id == userId,
                includeProperties: "Messages"
            );

            return chats.Any() ? Ok(chats) : NotFound("No chats found for the user");
        }

        // Delete a chat
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(Guid id)
        {
            var chat = await _unitOfWork.Chats.GetByIdAsync(id);
            if (chat == null)
                return NotFound("Chat not found");

            await _unitOfWork.Chats.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return Ok("Chat deleted successfully");
        }

        // Update chat name
        [HttpPatch("{id}/updateName")]
        public async Task<IActionResult> UpdateChatName(Guid id, [FromBody] string newChatName)
        {
            if (string.IsNullOrWhiteSpace(newChatName))
                return BadRequest("Chat name cannot be empty");

            var chat = await _unitOfWork.Chats.GetByIdAsync(id);
            if (chat == null)
                return NotFound("Chat not found");

            if (chat.ChatName == newChatName.Trim())
                return Ok("Chat name is already up to date");

            chat.ChatName = newChatName.Trim();
            await _unitOfWork.Chats.UpdateAsync(chat);
            await _unitOfWork.SaveAsync();

            return Ok("Chat name updated successfully");
        }
        // Get a chat by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChatById(Guid id)
        {
            var chat = await _unitOfWork.Chats.GetFirstOrDefaultAsync(
                c => c.Id == id,
                includeProperties: "Messages"
            );

            return chat != null ? Ok(chat) : NotFound("Chat not found");
        }

        // Get Messages by chat ID
        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessagesByChatId(Guid id, int page = 1, int pageSize = 20)
        {
            var chatExists = await _unitOfWork.Chats.GetByIdAsync(id);
            if (chatExists == null)
                return NotFound("Chat not found");

            var messages = await _unitOfWork.Messages.GetAllAsync(
                m => m.ChatId == id,
                orderBy: m => m.OrderBy(msg => msg.Timestamp),
                skip: (page - 1) * pageSize,
                take: pageSize
            );

            return messages.Any() ? Ok(messages) : NotFound("No messages found for this chat");
        }

        //Add a new message to a chat. Ensure the chat exists before adding.
        

        [HttpPost("{id}/messages")]
        public async Task<IActionResult> SendMessage(Guid id, [FromBody] Messages newMessage)
        {
            var chat = await _unitOfWork.Chats.GetByIdAsync(id);
            if (chat == null)
                return NotFound("Chat not found");

            if (string.IsNullOrWhiteSpace(newMessage.Content))
                return BadRequest("Message cannot be empty");

            var message = new Messages
            {
                ChatId = id,
                SenderId = newMessage.SenderId,
                ReceiverId = newMessage.ReceiverId,
                Content = newMessage.Content,
                Timestamp = DateTime.UtcNow,
                IsRead = false,
                IsChallenge = newMessage.IsChallenge
            };

            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.SaveAsync();

            // TODO: Trigger SignalR for real-time messaging
            // We can send this message object to the SignalR Hub for broadcasting.
            await _hubContext.Clients.Group(id.ToString()).SendAsync("ReceiveMessage", newMessage);
            return Ok(message);
        }

        [HttpPatch("{id}/messages/markAsRead")]
        public async Task<IActionResult> MarkMessagesAsRead(Guid id, [FromBody] List<int> messageIds)
        {
            var messages = await _unitOfWork.Messages.GetAllAsync(m => messageIds.Contains(m.Id) && m.ChatId == id);

            if (!messages.Any())
                return NotFound("No messages found to update");

            foreach (var message in messages)
                message.IsRead = true;

            await _unitOfWork.Messages.UpdateRangeAsync(messages);
            await _unitOfWork.SaveAsync();

            return Ok("Messages marked as read");
        }

        

    }
}
