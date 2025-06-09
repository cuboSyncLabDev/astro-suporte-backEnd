using AstroBot.Application.DTOs.Requests;
using AstroBot.Application.DTOs.Responses;

namespace AstroBot.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseBase<LoginResponseData>> LoginAsync(LoginRequest request);
        Task<ResponseBase<MessageResponseData>> RegisterAsync(RegisterUserRequest request);
        Task<ResponseBase<MessageResponseData>> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
