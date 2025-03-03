namespace LibraryManager.Application.Models.ViewModels;

public class BooksViewModel
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int? BorrowerId { get; private set; }
    

    public BooksViewModel(int id, string title, string author, int? borrowerId)
    {
        Id = id;
        Title = title;
        Author = author;
        BorrowerId = borrowerId;
    }
    
    public static BooksViewModel FromEntity(Domain.Entities.Book book)=>
    new (book.Id, book.Title, book.Author, book.CurrentLoan?.IdUser);
}