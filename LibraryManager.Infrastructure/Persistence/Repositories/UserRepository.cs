using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Persistence.Repositories;

public class UserRepository(LibraryManagerDbContext context) : IUserRepository
{
    public async Task<User?> GetUserDetailsByIdAsync(int userId)
    {
        var user = await context.Users
            .Include(u => u.Loans)
            .ThenInclude(loan => loan.Book)
            .SingleOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
        return user;
    }

    public async Task<int> AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        
        return user.Id;
    }
}