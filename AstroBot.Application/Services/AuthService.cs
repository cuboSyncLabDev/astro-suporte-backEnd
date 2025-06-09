using AstroBot.Application.DTOs.Requests;
using AstroBot.Application.DTOs.Responses;
using AstroBot.Application.Interfaces;
using AstroBot.Domain.Entities;
using AstroBot.Domain.Interfaces;
using AstroBot.Infrastructure.Services.Interfaces;
using System.Net;

namespace AstroBot.Application.Services
{
    public class AuthService(
        IUserRepository _userRepository, 
        IJwtProvider _jwtProvider
        ) : IAuthService
    {
        public async Task<ResponseBase<LoginResponseData>> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return ResponseBase<LoginResponseData>.Error("Invalid credentials", HttpStatusCode.Unauthorized);

            var token = _jwtProvider.GenerateToken(user);
            return ResponseBase<LoginResponseData>.Success(new LoginResponseData(token));
        }

        public async Task<ResponseBase<MessageResponseData>> RegisterAsync(RegisterUserRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return ResponseBase<MessageResponseData>.Error("User already exists", HttpStatusCode.BadRequest);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = passwordHash
            };

            await _userRepository.CreateAsync(user);
            return ResponseBase<MessageResponseData>.Success(new MessageResponseData("User registered successfully"));
        }

        public async Task<ResponseBase<MessageResponseData>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return ResponseBase<MessageResponseData>.Error("User not found", HttpStatusCode.NotFound);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _userRepository.UpdateAsync(user);
            return ResponseBase<MessageResponseData>.Success(new MessageResponseData("Password reset successfully"));
        }
    }
}
