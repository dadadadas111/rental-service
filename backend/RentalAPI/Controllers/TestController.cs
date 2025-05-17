using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentalAPI.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult PublicEndpoint()
        {
            return Ok("Public access ✅");
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
            return Ok($"Protected access ✅ – Hello {email}");
        }

        [Authorize(Roles = "Student")]
        [HttpGet("student")]
        public IActionResult StudentOnly()
        {
            return Ok("Hello, Student 🎓");
        }

        [Authorize(Roles = "Landlord")]
        [HttpGet("landlord")]
        public IActionResult LandlordOnly()
        {
            return Ok("Hello, Landlord 🏠");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Hello, Admin 🛠");
        }
    }
}
