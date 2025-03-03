using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class SingleLoanViewModel(
    string bookName, 
    string userName, 
    DateOnly deadLineDevolutionDate, 
    bool isReturned, 
    bool isLate, 
    DateOnly? returnedDate)
{
    public string BookName { get; } = bookName;
    public string UserName { get; } = userName;
    public DateOnly DeadLineDevolutionDate { get; } = deadLineDevolutionDate;
    public bool IsReturned { get; } = isReturned;
    public bool IsLate { get; } = isLate;
    public DateOnly? ReturnedDate { get; } = returnedDate;

    public static SingleLoanViewModel FromEntity(Loan loan)
    {
        return new SingleLoanViewModel(
            loan.Book.Title,
            loan.User.Name,
            DateOnly.FromDateTime(loan.DeadLineDevolutionDate),
            loan.IsReturned,
            loan.IsLate(),
            loan.ReturnedDate.HasValue ? DateOnly.FromDateTime(loan.ReturnedDate.Value) : null
        );
    }
}