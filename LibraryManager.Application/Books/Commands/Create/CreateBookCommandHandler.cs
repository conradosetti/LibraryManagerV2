using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;

namespace LibraryManager.Application.Books.Commands.Create;

public class CreateBookCommandHandler(LibraryManagerDbContext context)
: IRequestHandler<CreateBookCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.ToEntity();
        await context.Books.AddAsync(book, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return ResultViewModel<int>.Success(book.Id);
    }
}