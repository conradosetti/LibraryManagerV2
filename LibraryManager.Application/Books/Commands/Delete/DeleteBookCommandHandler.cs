using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Books.Commands.Delete;

public class DeleteBookCommandHandler(LibraryManagerDbContext context)
: IRequestHandler<DeleteBookCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await context.Books
            .SingleOrDefaultAsync(b => b.Id == request.Id || !b.IsDeleted, cancellationToken: cancellationToken);
        if (book == null)
            return ResultViewModel.Error("Book not found");
        book.SetAsDeleted();
        context.Books.Update(book);
        await context.SaveChangesAsync(cancellationToken);
        
        return ResultViewModel.Success();
    }
}