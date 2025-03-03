using LibraryManager.Domain.Entities;

namespace LibraryManager.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserDetailsByIdAsync(int userId);
    Task<int> AddUserAsync(User user);
}