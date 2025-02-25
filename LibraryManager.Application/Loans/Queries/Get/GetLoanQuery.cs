using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Loans.Queries.Get;

public class GetLoanQuery(int id) : IRequest<ResultViewModel<SingleLoanViewModel>>
{
    public int Id { get; set; } = id;
}