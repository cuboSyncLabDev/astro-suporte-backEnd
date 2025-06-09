using AstroBot.Domain.Entities;

namespace AstroBot.Infrastructure.Services.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
