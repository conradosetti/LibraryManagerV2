using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Books.Commands.Create;

public class CreateBookCommandHandler(IBookRepository repository)
: IRequestHandler<CreateBookCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.ToEntity();
        await repository.AddBookAsync(book);
        
        return ResultViewModel<int>.Success(book.Id);
    }
}