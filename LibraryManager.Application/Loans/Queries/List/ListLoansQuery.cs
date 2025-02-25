using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Loans.Queries.List;

public class ListLoansQuery(string search) : IRequest<ResultViewModel<List<LoansViewModel>>>
{
    public string Search { get; set; } = search;
}