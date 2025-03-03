using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Persistence.Repositories;

public class LoanRepository(LibraryManagerDbContext context) : ILoanRepository
{
    public async Task<List<Loan>> ListLoansAsync(string search)
    {
        var loans = await context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .Where(l => !l.IsDeleted && 
                        (l.User.Name.Contains(search) || l.Book.Title.Contains(search) || search == ""))
            .ToListAsync();
        const string a = "";
        var b = "test".Contains(a);
        return loans;
    }

    public async Task<Loan?> GetLoanByIdAsync(int id)
    {
        var loan  = await context.Loans.SingleOrDefaultAsync(l => l.Id == id && l.IsDeleted == false);
        return loan;
    }

    public async Task<Loan?> GetLoanDetailsByIdAsync(int id)
    {
        var loan  = await context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .SingleOrDefaultAsync(l => l.Id == id && l.IsDeleted == false);
        return loan;
    }

    public async Task<int> AddLoanAsync(Loan loan)
    {
        await context.Loans.AddAsync(loan);
        await context.SaveChangesAsync();
        return loan.Id;
    }

    public async Task UpdateLoanAsync(Loan loan)
    {
        context.Loans.Update(loan);
        await context.SaveChangesAsync();
    }
}