using AstroBot.Application.DTOs.Requests;
using AstroBot.Application.DTOs.Responses;

namespace AstroBot.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterUserRequest request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
