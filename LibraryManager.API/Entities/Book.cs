namespace LibraryManager.API.Entities;

public class Book : BaseEntity
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Isbn { get; private set; }
    public DateTime PublishDate { get; private set; }
    public bool IsBorrowed { get; private set; } = false;
    public List<Loan> Loans { get; private set; } = [];

    public Book(string title, string author, string isbn, DateTime publishDate) : base()
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        PublishDate = publishDate;
    }

    public void ChangeStatus()
    {
        IsBorrowed = !IsBorrowed;
    }
    
    
}