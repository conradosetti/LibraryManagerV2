using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Books.Commands.Delete;

public class DeleteBookCommandHandler(IBookRepository repository)
: IRequestHandler<DeleteBookCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await repository.GetBookByIdAsync(request.Id);
        if (book == null)
            return ResultViewModel.Error("Book not found");
        book.SetAsDeleted();
        await repository.UpdateBookAsync(book);
        
        return ResultViewModel.Success();
    }
}