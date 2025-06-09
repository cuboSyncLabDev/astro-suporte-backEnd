using AstroBot.Domain.Entities;
using AstroBot.Domain.Interfaces;
using AstroBot.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AstroBot.Persistence.Repositories
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
