using LibraryManager.Application.Models.InputModels;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Services;

public class LoanService(LibraryManagerDbContext context) : ILoanService
{
    public async Task<ResultViewModel<SingleLoanViewModel>> GetLoanById(int id)
    {
        var loan = await context.Loans
            .Include(l=>l.Book)
            .Include(l=>l.User)
            .SingleOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        if (loan == null)
            return ResultViewModel<SingleLoanViewModel>.Error("Loan not found");
        var model = SingleLoanViewModel.FromEntity(loan);
        
        return ResultViewModel<SingleLoanViewModel>.Success(model);
    }

    public async Task<ResultViewModel<List<LoansViewModel>>> ListLoans(string search = "")
    {
        var loans = await context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .Where
            (l => !l.IsDeleted && 
                  (search == "" || l.Book.Title.Contains(search) || l.User.Name.Contains(search)))
            .ToListAsync();
        var models = loans.Select
            (l => LoansViewModel.FromEntity(l)).ToList();
        
        return ResultViewModel<List<LoansViewModel>>.Success(models);
    }

    public async Task<ResultViewModel<List<int>>> CreateLoan(CreateLoanInputModel model)
    {
        var books = await context.Books.Where(b => !b.IsDeleted && model.IdBooks.Contains(b.Id)).ToListAsync();
        if (books.Count == 0)
            return ResultViewModel<List<int>>.Error("No books found with the given id");
        if (books.Any(b => b.IsBorrowed))
        {
            return ResultViewModel<List<int>>.Error("At least one book is borrowed");
        }
        
        var loans = model.ToEntity();
        context.Loans.AddRange(loans);
        
        books.ForEach(b => b.ChangeStatus());
        await context.SaveChangesAsync();
        
        return ResultViewModel<List<int>>.Success(loans.Select(l => l.Id).ToList());
    }
}