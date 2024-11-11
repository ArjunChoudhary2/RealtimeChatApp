using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtimeChatApp.RealtimeChatApp.Business.Services;

namespace RealtimeChatApp.RealtimeChatApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public TestController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserData()
        {
            try
            {
                // Call a method from DatabaseService to interact with the database
                await _databaseService.GetUserDataAsync();

                // Return a success response
                return Ok(new { message = "User data retrieved successfully" });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, new { message = "Error retrieving data", details = ex.Message });
            }
        }
    }
}
