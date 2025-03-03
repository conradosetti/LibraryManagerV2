using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Loans.Queries.List;

public class ListLoansQueryHandler(ILoanRepository repository)
: IRequestHandler<ListLoansQuery, ResultViewModel<List<LoansViewModel>>>
{
    public async Task<ResultViewModel<List<LoansViewModel>>> Handle(ListLoansQuery request, CancellationToken cancellationToken)
    {
        var loans = await repository.ListLoansAsync(request.Search);
        var models = loans.Select
            (LoansViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<LoansViewModel>>.Success(models);
    }
}