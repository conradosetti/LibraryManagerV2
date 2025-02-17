namespace LibraryManager.API.Models;

public class CreateLoanInputModel
{
    public int[] IdBooks { get; set; }
    public int IdUser { get; set; }
    public DateTime DevolutionDate { get; set; }
}