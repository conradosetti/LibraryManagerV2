using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Books.Queries.Get;

public class GetBookQueryHandler(LibraryManagerDbContext context, IBookRepository repository)
: IRequestHandler<GetBookQuery, ResultViewModel<SingleBookViewModel>>
{
    public async Task<ResultViewModel<SingleBookViewModel>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await repository.GetBookDetailsByIdAsync(request.Id);
        if (book == null)
            return ResultViewModel<SingleBookViewModel>.Error("Book not found");
        var model = SingleBookViewModel.FromEntity(book);
        
        return ResultViewModel<SingleBookViewModel>.Success(model);
    }
}