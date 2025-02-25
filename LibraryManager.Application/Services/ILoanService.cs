using LibraryManager.Application.Models.InputModels;
using LibraryManager.Application.Models.ViewModels;

namespace LibraryManager.Application.Services;

public interface ILoanService
{
    public Task<ResultViewModel<SingleLoanViewModel>> GetLoanById(int id);
    public Task<ResultViewModel<List<LoansViewModel>>> ListLoans(string search = "");
    public Task<ResultViewModel<List<int>>> CreateLoan(CreateLoanInputModel model);
}