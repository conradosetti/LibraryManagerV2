using LibraryManager.Application.Models.InputModels;
using LibraryManager.Application.Models.ViewModels;

namespace LibraryManager.Application.Services;

public interface IUserService
{
    public Task<ResultViewModel<SingleUserViewModel>> GetUserByIdAsync(int id);
    public Task<ResultViewModel<int>> CreateUserAsync(CreateUserInputModel model);
}