namespace LibraryManager.API.Entities;

public class Loan : BaseEntity
{
    public int IdUser { get; private set; }
    public User User { get; private set; }
    public int IdBook { get; private set; }
    public Book Book { get; private set; }
    public bool IsReturned { get; private set; } = false;
    public DateTime DevolutionDate { get; private set; }

    public Loan(int idUser, int idBook, DateTime devolutionDate) : base()
    {
        IdUser = idUser;
        IdBook = idBook;
        DevolutionDate = devolutionDate;
    }
    
    public bool IsLate()
    {
        return DateTime.Now >= DevolutionDate;
    }
}