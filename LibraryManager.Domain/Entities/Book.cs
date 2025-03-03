namespace LibraryManager.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Isbn { get; private set; }
    public DateTime PublishDate { get; private set; }
    public int? CurrentLoanId { get; private set; } = null;
    public Loan? CurrentLoan { get; private set; } = null;
    public List<Loan> Loans { get; private set; } = [];

    public Book(string title, string author, string isbn, DateTime publishDate) : base()
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        PublishDate = publishDate;
    }
    
    public void BorrowBook(Loan loan)
    {
        if (CurrentLoanId != null)
            throw new InvalidOperationException("Book is already borrowed.");

        CurrentLoan = loan;
        CurrentLoanId = loan.Id;
    }
    
    public void ReturnBook()
    {
        if (CurrentLoanId == null)
            throw new InvalidOperationException("Book is not currently borrowed.");

        CurrentLoanId = null;
        CurrentLoan = null;
    }
    
}