using LibraryManager.Domain.Entities;

namespace LibraryManager.Domain.Repositories;

public interface IBookRepository
{
    Task<List<Book>> ListBooksAsync(string search);
    Task<List<Book>> ListBooksByIdAsync(int[] id);
    Task<Book?> GetBookByIdAsync(int bookId);
    Task<Book?> GetBookDetailsByIdAsync(int bookId);
    Task<int> AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
}