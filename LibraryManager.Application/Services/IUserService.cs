using LibraryManager.Application.Models;

namespace LibraryManager.Application.Services;

public interface IUserService
{
    public Task<ResultViewModel<SingleUserViewModel>> GetUserByIdAsync(int id);
    public Task<ResultViewModel<int>> CreateUserAsync(CreateUserInputModel model);
}