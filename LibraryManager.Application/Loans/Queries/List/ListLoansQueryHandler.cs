using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Loans.Queries.List;

public class ListLoansQueryHandler(LibraryManagerDbContext context)
: IRequestHandler<ListLoansQuery, ResultViewModel<List<LoansViewModel>>>
{
    public async Task<ResultViewModel<List<LoansViewModel>>> Handle(ListLoansQuery request, CancellationToken cancellationToken)
    {
        var loans = await context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .Where
            (l => !l.IsDeleted && 
                  (request.Search == "" || l.Book.Title.Contains(request.Search) || l.User.Name.Contains(request.Search)))
            .ToListAsync(cancellationToken: cancellationToken);
        var models = loans.Select
            (LoansViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<LoansViewModel>>.Success(models);
    }
}