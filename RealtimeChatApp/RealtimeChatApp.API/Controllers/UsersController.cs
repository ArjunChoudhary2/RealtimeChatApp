using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;

namespace RealtimeChatApp.RealtimeChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user =  await _unitOfWork.Users.GetByIdAsync(id);
            return user != null ? Ok(user) : NotFound("User not found");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query cannot be empty");

            var users = await _unitOfWork.Users.GetAllAsync(u => u.Username.Contains(query.Trim()));
            return Ok(users);
        }


        [HttpPut("{id}/updateProfile")]
        public async Task<IActionResult> UpdateUserProfile(Guid id, [FromBody] Users updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            // Update user properties selectively
            user.Username = updatedUser.Username?.Trim();
            user.ProfilePicture = updatedUser.ProfilePicture;
            user.MoodStatus = updatedUser.MoodStatus?.Trim();

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            return Ok("Profile updated successfully");
        }

        [HttpPatch("{id}/setMood")]
        public async Task<IActionResult> SetMoodStatus(Guid id, [FromBody] string moodStatus)
        {
            if (string.IsNullOrWhiteSpace(moodStatus))
                return BadRequest("Mood status cannot be empty");

            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            if (user.MoodStatus == moodStatus.Trim())
                return Ok("Mood status is already up to date");

            user.MoodStatus = moodStatus.Trim();
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            return Ok("Mood status updated successfully");
        }

        [HttpPost("createProfile")]
        public async Task<IActionResult> CreateProfile([FromBody] Users newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid user data");

            // Check if user already exists by username or other criteria
            var existingUser = await _unitOfWork.Users.GetFirstOrDefaultAsync(u => u.Username == newUser.Username);
            if (existingUser != null)
                return Conflict("A user with this username already exists");

            // Create new user
            var user = new Users
            {
                Id = Guid.NewGuid(),
                Username = newUser.Username?.Trim(),
                ProfilePicture = newUser.ProfilePicture,
                MoodStatus = newUser.MoodStatus?.Trim(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }


    }
}
