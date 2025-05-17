using Microsoft.AspNetCore.Mvc;
using RentalAPI.Services;

namespace RentalAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwt;

        public AuthController(JwtService jwt)
        {
            _jwt = jwt;
        }

        [HttpPost("fake-login")]
        public IActionResult FakeLogin([FromQuery] string role = "Student")
        {
            if (role != "Student" && role != "Landlord" && role != "Admin")
                return BadRequest("Role must be Student, Landlord, or Admin.");

            var fakeUserId = Guid.NewGuid().ToString();
            var fakeEmail = $"{role.ToLower()}@example.com";
            var token = _jwt.GenerateToken(fakeUserId, fakeEmail, role);

            return Ok(new
            {
                token,
                role,
                email = fakeEmail
            });
        }
    }
}
