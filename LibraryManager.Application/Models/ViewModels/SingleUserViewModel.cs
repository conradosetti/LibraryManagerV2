using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class SingleUserViewModel(string name, string email, LoanedBookViewModel[] books)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public LoanedBookViewModel[] Books { get; set; } = books;

    public static SingleUserViewModel FromEntity(User user)
    {
        var books = user.Loans
            .Select(l => new LoanedBookViewModel(l.Book.Title, l.CreatedAt))
            .ToArray();
        return new SingleUserViewModel(user.Name, user.Email, books);
    }
}

public class LoanedBookViewModel(string title, DateTime loanDate)
{
    public string Title { get; init; } = title;
    public DateTime LoanDate { get; init; } = loanDate;
}

