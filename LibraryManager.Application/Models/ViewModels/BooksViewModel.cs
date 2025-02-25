namespace LibraryManager.Application.Models.ViewModels;

public class BooksViewModel
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string IsBorrowed { get; private set; }
    

    public BooksViewModel(string title, string author, string isBorrowed)
    {
        Title = title;
        Author = author;
        IsBorrowed = isBorrowed;
    }
    
    public static BooksViewModel FromEntity(Domain.Entities.Book book)=>
    new (book.Title, book.Author, book.IsBorrowed? "Borrowed Book" : "Not Borrowed Book");
}