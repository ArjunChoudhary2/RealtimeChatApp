using Microsoft.AspNetCore.Mvc;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            var chat = new Chats
            {
                Id = Guid.NewGuid(),
                User1_Id = newChat.User1_Id,
                User2_Id = newChat.User2_Id,
                ChatName = newChat.ChatName?.Trim(),
                CreatedAt = DateTime.UtcNow
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

        // Delete a chat
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(Guid id)
        {
            var chat = await _unitOfWork.Chats.GetByIdAsync(id);
            if (chat == null)
                return NotFound("Chat not found");

            _unitOfWork.Chats.DeleteAsync(id);
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
    }
}
