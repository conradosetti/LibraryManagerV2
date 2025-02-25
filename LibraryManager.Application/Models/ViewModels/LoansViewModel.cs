using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class LoansViewModel(string userName, string bookName, DateTime loanDate)
{
    public string UserName { get; private set; } = userName;
    public string BookName { get; private set; } = bookName;
    public DateTime LoanDate { get; private set; } = loanDate;

    public static LoansViewModel FromEntity(Loan loan)
    {
        return new LoansViewModel(loan.User.Name, loan.Book.Title, loan.CreatedAt);
    }
}