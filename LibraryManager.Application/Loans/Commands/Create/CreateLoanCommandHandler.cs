using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Loans.Commands.Create;

public class CreateLoanCommandHandler(LibraryManagerDbContext context)
: IRequestHandler<CreateLoanCommand, ResultViewModel<List<int>>>
{
    public async Task<ResultViewModel<List<int>>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var books = await context.Books.Where(b => !b.IsDeleted && request.IdBooks.Contains(b.Id)).ToListAsync(cancellationToken: cancellationToken);
        if (books.Count == 0)
            return ResultViewModel<List<int>>.Error("No books found with the given id");
        if (books.Any(b => b.IsBorrowed))
        {
            return ResultViewModel<List<int>>.Error("At least one book is borrowed");
        }
        
        var loans = request.ToEntity();
        context.Loans.AddRange(loans);
        
        books.ForEach(b => b.ChangeStatus());
        await context.SaveChangesAsync(cancellationToken);
        
        return ResultViewModel<List<int>>.Success(loans.Select(l => l.Id).ToList());
    }
}