using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Books.Queries.Get;

public class GetBookQueryHandler(LibraryManagerDbContext context)
: IRequestHandler<GetBookQuery, ResultViewModel<SingleBookViewModel>>
{
    public async Task<ResultViewModel<SingleBookViewModel>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await context.Books
            .Include(b => b.Loans)
            .SingleOrDefaultAsync(b => b.Id == request.Id && !b.IsDeleted, cancellationToken: cancellationToken);
        if (book == null)
            return ResultViewModel<SingleBookViewModel>.Error("Book not found");
        var model = SingleBookViewModel.FromEntity(book);
        
        return ResultViewModel<SingleBookViewModel>.Success(model);
    }
}