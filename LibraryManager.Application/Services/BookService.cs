using LibraryManager.Application.Models.InputModels;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Services;

public class BookService(LibraryManagerDbContext context) : IBookService
{
    public async Task<ResultViewModel<List<BooksViewModel>>> ListBooksAsync(string search ="")
    {
        var books = await context.Books
            .Where
                (b => (search == "" || b.Title.Contains(search) || b.Author.Contains(search)) && !b.IsDeleted)
            .ToListAsync();
        var model = books.Select(b => BooksViewModel.FromEntity(b)).ToList();
        
        return ResultViewModel<List<BooksViewModel>>.Success(model);
    }

    public async Task<ResultViewModel<SingleBookViewModel>> GetBookByIdAsync(int id)
    {
        var book = await context.Books
            .Include(b => b.Loans)
            .SingleOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        if (book == null)
            return ResultViewModel<SingleBookViewModel>.Error("Book not found");
        var model = SingleBookViewModel.FromEntity(book);
        
        return ResultViewModel<SingleBookViewModel>.Success(model);
    }

    public async Task<ResultViewModel<int>> CreateBookAsync(CreateBookInputModel model)
    {
        var book = model.ToEntity();
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        
        return ResultViewModel<int>.Success(book.Id);
    }

    public async Task<ResultViewModel> DeleteBookAsync(int id)
    {
        var book = await context.Books
            .SingleOrDefaultAsync(b => b.Id == id || !b.IsDeleted);
        if (book == null)
            return ResultViewModel.Error("Book not found");
        book.SetAsDeleted();
        context.Books.Update(book);
        await context.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel> GiveBackBookAsync(int id)
    {
        var book = await context.Books
            .Include(b => b.Loans)
            .SingleOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        if (book == null)
            return ResultViewModel.Error("Book not found");
        if(!book.IsBorrowed)
            return ResultViewModel.Error("Book is not borrowed");
        var loan = await context.Loans.SingleOrDefaultAsync(l=>l.IdBook == id && !l.IsReturned);
        book.ChangeStatus();
        context.Books.Update(book);
        if (loan.IsLate())
        {
            loan.ReturnBook();
            context.Loans.Update(loan);
            await context.SaveChangesAsync();
            return new ResultViewModel(true, "Loan is late");
        }
        loan.ReturnBook();
        context.Loans.Update(loan);
        await context.SaveChangesAsync();
        return new ResultViewModel(true, "Loan is on time");
    }
}