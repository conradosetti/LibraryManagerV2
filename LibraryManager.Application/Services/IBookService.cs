using LibraryManager.Application.Models.InputModels;
using LibraryManager.Application.Models.ViewModels;

namespace LibraryManager.Application.Services;

public interface IBookService
{
    Task<ResultViewModel<List<BooksViewModel>>> ListBooksAsync(string search = "");
    Task<ResultViewModel<SingleBookViewModel>> GetBookByIdAsync(int id);
    Task<ResultViewModel<int>> CreateBookAsync(CreateBookInputModel model);
    Task<ResultViewModel> DeleteBookAsync(int id);
    Task<ResultViewModel> GiveBackBookAsync(int id);
}