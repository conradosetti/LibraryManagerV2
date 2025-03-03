using LibraryManager.Domain.Entities;

namespace LibraryManager.Domain.Repositories;

public interface ILoanRepository
{
    Task<List<Loan>> ListLoansAsync(string search);
    Task<Loan?> GetLoanByIdAsync(int id);
    Task<Loan?> GetLoanDetailsByIdAsync(int id);
    Task<int> AddLoanAsync(Loan loan);
    Task UpdateLoanAsync(Loan loan);
}