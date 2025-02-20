using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models;

public class SingleLoanViewModel(string bookName, string userName, string isReturned, string isLate)
{
    public string BookName { get; private set; } = bookName;
    public string UserName { get; private set; } = userName;
    public string IsReturned { get; private set; } = isReturned;
    public string IsLate { get; private set; } = isLate;

    public static SingleLoanViewModel FromEntity(Loan loan)
    {
        return new SingleLoanViewModel(loan.Book.Title, loan.User.Name, loan.IsReturned ? "Book was returned" : "Still borrowed book", loan.IsLate() ? "Late" : "Not late");
    }
}