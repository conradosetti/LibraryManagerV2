using LibraryManager.Application.Models;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Services;

public class UserService(LibraryManagerDbContext context) : IUserService
{
    public async Task<ResultViewModel<SingleUserViewModel>> GetUserByIdAsync(int id)
    {
        var user = await context.Users
            .Include(u => u.Loans).ThenInclude(loan => loan.Book)
            .SingleOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        if (user == null)
            return ResultViewModel<SingleUserViewModel>.Error("User not found");
        
        var model = SingleUserViewModel.FromEntity(user);
        return ResultViewModel<SingleUserViewModel>.Success(model);
    }

    public async Task<ResultViewModel<int>> CreateUserAsync(CreateUserInputModel model)
    {
        var user = model.ToEntity();
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        
        return ResultViewModel<int>.Success(user.Id);
    }
}