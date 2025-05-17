using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalAPI.Data;
using RentalAPI.Models;
using RentalAPI.Models.DTOs;
using RentalAPI.Services;

namespace RentalAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwt;
        private readonly AppDbContext _context;


        public AuthController(JwtService jwt, AppDbContext context)
        {
            _jwt = jwt;
            _context = context;
        }

        [HttpPost("fake-login")]
        public IActionResult FakeLogin([FromQuery] string role = "Student")
        {
            if (role != "Student" && role != "Landlord" && role != "Admin")
                return BadRequest("Role must be Student, Landlord, or Admin.");

            var fakeUserId = Guid.NewGuid().ToString();
            var fakeEmail = $"{role.ToLower()}@example.com";
            var token = _jwt.GenerateFakeToken(fakeUserId, fakeEmail, role);

            return Ok(new
            {
                token,
                role,
                email = fakeEmail
            });
        }

        [HttpPost("google")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO body, [FromQuery] string? role)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(body.IdToken);

            var email = payload.Email;
            var fullName = payload.Name;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                var finalRole = role ?? body.Role ?? "Student";

                user = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = fullName,
                    Email = email,
                    Role = finalRole,
                    PhoneNumber = ""
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            var token = _jwt.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
