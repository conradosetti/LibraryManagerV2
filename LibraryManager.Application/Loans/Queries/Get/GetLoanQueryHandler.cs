using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Loans.Queries.Get;

public class GetLoanQueryHandler(LibraryManagerDbContext context)
: IRequestHandler<GetLoanQuery, ResultViewModel<SingleLoanViewModel>>
{
    public async Task<ResultViewModel<SingleLoanViewModel>> Handle(GetLoanQuery request, CancellationToken cancellationToken)
    {
        var loan = await context.Loans
            .Include(l=>l.Book)
            .Include(l=>l.User)
            .SingleOrDefaultAsync(l => l.Id == request.Id && !l.IsDeleted, cancellationToken: cancellationToken);
        if (loan == null)
            return ResultViewModel<SingleLoanViewModel>.Error("Loan not found");
        var model = SingleLoanViewModel.FromEntity(loan);
        
        return ResultViewModel<SingleLoanViewModel>.Success(model);
    }
}