using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Loans.Commands.Create;

public class CreateLoanCommand : IRequest<ResultViewModel<List<int>>>
{
    public int[] IdBooks { get; set; }
    public int IdUser { get; set; }
    public DateTime DevolutionDate { get; set; }

    public List<Loan> ToEntity()
    {
        return IdBooks.Select(idBook => new Loan(IdUser, idBook, DevolutionDate)).ToList();
    }
}