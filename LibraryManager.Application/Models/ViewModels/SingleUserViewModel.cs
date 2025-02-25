using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class SingleUserViewModel(string name, string email, (string, DateTime)[] books)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public (string, DateTime)[] Books { get; set; } = books;

    public static SingleUserViewModel FromEntity(User user)
    {
        (string, DateTime)[] books = user.Loans.Select(l => (l.Book.Title, l.CreatedAt)).ToArray();
        return new SingleUserViewModel(user.Name, user.Email, books);
    }
}