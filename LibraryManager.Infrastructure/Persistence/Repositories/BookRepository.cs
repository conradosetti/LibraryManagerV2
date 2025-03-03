using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Persistence.Repositories;

public class BookRepository(LibraryManagerDbContext context) : IBookRepository
{
    public async Task<List<Book>> ListBooksAsync(string search)
    {
        var books = await context.Books
            .Include(b => b.CurrentLoan)
            .ThenInclude(l => l.User)
            .Where
                (b => (search == "" || b.Title.Contains(search) || b.Author.Contains(search)) && !b.IsDeleted)
            .ToListAsync();
        return books;
    }

    public async Task<List<Book>> ListBooksByIdAsync(int[] id)
    {
        var books = await context.Books.Where(b => !b.IsDeleted && id.Contains(b.Id)).ToListAsync();
        return books;
    }

    public async Task<Book?> GetBookByIdAsync(int bookId)
    {
        var book = await context.Books
            .SingleOrDefaultAsync(b => b.Id == bookId && !b.IsDeleted);
        return book;
    }

    public async Task<Book?> GetBookDetailsByIdAsync(int bookId)
    {
        var book = await context.Books
            .Include(b => b.Loans)
            .SingleOrDefaultAsync(b => b.Id == bookId && !b.IsDeleted);
        return book;
    }


    public async Task<int> AddBookAsync(Book book)
    {
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        
        return book.Id;
    }

    public async Task UpdateBookAsync(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
    }
}