using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.InputModels;

public class CreateLoanInputModel
{
    public int[] IdBooks { get; set; }
    public int IdUser { get; set; }
    public DateTime DevolutionDate { get; set; }

    public List<Loan> ToEntity()
    {
        return IdBooks.Select(idBook => new Loan(IdUser, idBook, DevolutionDate)).ToList();
    }
}