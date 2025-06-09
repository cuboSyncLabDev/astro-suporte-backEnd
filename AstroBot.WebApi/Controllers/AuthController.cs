using AstroBot.Application.DTOs.Requests;
using AstroBot.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AstroBot.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result)
                return BadRequest(new { message = "User already exists" });

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);

            if (!result)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "Password reset successfully" });
        }
    }
}
