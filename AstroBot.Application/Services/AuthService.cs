using AstroBot.Application.DTOs.Requests;
using AstroBot.Application.DTOs.Responses;
using AstroBot.Application.Interfaces;
using AstroBot.Domain.Entities;
using AstroBot.Domain.Interfaces;
using AstroBot.Infrastructure.Services.Interfaces;

namespace AstroBot.Application.Services
{
    public class AuthService(
        IUserRepository _userRepository, 
        IJwtProvider _jwtProvider
        ) : IAuthService
    {
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            var token = _jwtProvider.GenerateToken(user);

            return new LoginResponse(token);
        }

        public async Task<bool> RegisterAsync(RegisterUserRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return false;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = passwordHash
            };

            await _userRepository.CreateAsync(user);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
