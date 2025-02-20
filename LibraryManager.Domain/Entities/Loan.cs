namespace LibraryManager.Domain.Entities;

public class Loan : BaseEntity
{
    public int IdUser { get; private set; }
    public User User { get; private set; }
    public int IdBook { get; private set; }
    public Book Book { get; private set; }
    public bool IsReturned { get; private set; } = false;
    public DateTime DeadLineDevolutionDate { get; private set; }
    public DateTime? ReturnedDate { get; private set; } = null;

    public Loan(int idUser, int idBook, DateTime deadLineDevolutionDate) : base()
    {
        IdUser = idUser;
        IdBook = idBook;
        DeadLineDevolutionDate = deadLineDevolutionDate;
    }

    public void ReturnBook()
    {
        IsReturned = true;
        ReturnedDate = DateTime.Now;
    }
    
    public bool IsLate()
    {
        if(!IsReturned)
            return DateTime.Now >= DeadLineDevolutionDate;
        return ReturnedDate.HasValue && ReturnedDate.Value >= DeadLineDevolutionDate;
    }
}