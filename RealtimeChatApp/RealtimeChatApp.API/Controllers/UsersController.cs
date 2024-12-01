using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository;
using RealtimeChatApp.RealtimeChatApp.DataAccess.Repository.IRepository;
using RealtimeChatApp.RealtimeChatApp.Domain.Entities;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using Magic;

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
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return user != null ? Ok(user) : NotFound("User not found");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query cannot be empty");

            var users = await _unitOfWork.Users.GetAllAsync(u => u.Username.Contains(query.Trim()));

            // Check if no users were found
            if (users == null || !users.Any())
                return NotFound("Username not found");

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

        [HttpPost("{id}/uploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Validate the user exists
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Copy file to memory stream
                    await file.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    // Validate file type (optional, e.g., only images)
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                        return BadRequest("Invalid file type. Only image files are allowed.");

                    // Store the file as a blob in the database
                    user.ProfilePicture = fileBytes;
                    await _unitOfWork.Users.UpdateAsync(user);
                    await _unitOfWork.SaveAsync();
                }

                return Ok("Profile picture uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("user/picture/{id}")]
        public async Task<IActionResult> GetUserProfilePicture(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null || user.ProfilePicture == null)
            {
                return NotFound("User not found or user doesn't have profile picture.");
            }

            // Manually determine the MIME type based on the file extension
            var mimeType = GetMimeTypeFromExtension(".jpg"); // You can use any file extension you know from the profile picture.

            // Return the profile picture as a file response
            return File(user.ProfilePicture, mimeType);
        }


        private string GetMimeTypeFromExtension(string fileExtension)
        {
            var mimeTypes = new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".bmp", "image/bmp" },
                { ".pdf", "application/pdf" },
                // Add more MIME types as needed
            };

            // Check if the file extension exists in the dictionary, otherwise return a default MIME type
            return mimeTypes.ContainsKey(fileExtension.ToLower())
                ? mimeTypes[fileExtension.ToLower()]
                : "application/octet-stream";
        }
    }
}
