using LibraryManager.API.Entities;

namespace LibraryManager.API.Models;

public class SingleBookViewModel( 
    string title,
    string author,
    string isbn,
    DateTime publishDate,
    string isBorrowed,
    DateTime? devolutionDate)
{
    public string Title { get; private set; } = title;
    public string Author { get; private set; } = author;
    public string Isbn { get; private set; } = isbn;
    public DateTime PublishDate { get; private set; } = publishDate;
    public string IsBorrowed { get; private set; } = isBorrowed;
    public DateTime? DevolutionDate { get; private set; } = devolutionDate;

    public static SingleBookViewModel FromEntity(Book book)
    {
        var devolutionDate = book.Loans.SingleOrDefault(l => l.IdBook == book.Id && !l.IsReturned)?.DeadLineDevolutionDate;
        return new SingleBookViewModel(book.Title, book.Author, book.Isbn, book.PublishDate,
                book.IsBorrowed? "Borrowed Book": "Not borrowed Book", devolutionDate);
    }
}