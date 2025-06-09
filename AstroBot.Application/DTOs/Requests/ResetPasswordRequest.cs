namespace AstroBot.Application.DTOs.Requests
{
    public record ResetPasswordRequest(string Email, string NewPassword);
} 