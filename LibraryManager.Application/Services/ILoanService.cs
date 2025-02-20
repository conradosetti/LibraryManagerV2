using LibraryManager.Application.Models;

namespace LibraryManager.Application.Services;

public interface ILoanService
{
    public Task<ResultViewModel<SingleLoanViewModel>> GetLoanById(int id);
    public Task<ResultViewModel<List<LoansViewModel>>> GetAllLoans(string search = "");
    public Task<ResultViewModel<List<int>>> CreateLoan(CreateLoanInputModel model);
}