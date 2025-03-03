using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class LoansViewModel(int id, string userName, string bookName, bool isReturned)
{
    public int Id { get; private set; } = id;
    public string UserName { get; private set; } = userName;
    public string BookTitle { get; private set; } = bookName;
    public bool IsReturned { get; set; } = isReturned;

    public static LoansViewModel FromEntity(Loan loan)
    {
        return new LoansViewModel(loan.Id, loan.User.Name, loan.Book.Title, loan.IsReturned);
    }
}