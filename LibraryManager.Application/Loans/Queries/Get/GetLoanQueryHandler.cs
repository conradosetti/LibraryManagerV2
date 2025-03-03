using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Loans.Queries.Get;

public class GetLoanQueryHandler(ILoanRepository repository)
: IRequestHandler<GetLoanQuery, ResultViewModel<SingleLoanViewModel>>
{
    public async Task<ResultViewModel<SingleLoanViewModel>> Handle(GetLoanQuery request, CancellationToken cancellationToken)
    {
        var loan = await repository.GetLoanDetailsByIdAsync(request.Id);
        if (loan == null)
            return ResultViewModel<SingleLoanViewModel>.Error("Loan not found");
        var model = SingleLoanViewModel.FromEntity(loan);
        
        return ResultViewModel<SingleLoanViewModel>.Success(model);
    }
}