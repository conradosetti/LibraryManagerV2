namespace LibraryManager.API.Entities;

public class Book : BaseEntity
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Isbn { get; private set; }
    public DateTime PublishDate { get; private set; } = DateTime.Now;
    public bool IsBorrowed { get; private set; } = false;
    public Loan? Loan { get; private set; } = null;

    public Book(string title, string author, string isbn) : base()
    {
        Title = title;
        Author = author;
        Isbn = isbn;
    }

    public void ChangeStatus()
    {
        IsBorrowed = !IsBorrowed;
    }
    
    
    
}