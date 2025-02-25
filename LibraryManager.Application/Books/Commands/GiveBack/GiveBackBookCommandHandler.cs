using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Books.Commands.GiveBack;

public class GiveBackBookCommandHandler(LibraryManagerDbContext context) : IRequestHandler<GivebackBookCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(GivebackBookCommand request, CancellationToken cancellationToken)
    {
        var book = await context.Books
            .Include(b => b.Loans)
            .SingleOrDefaultAsync(b => b.Id == request.Id && !b.IsDeleted, cancellationToken: cancellationToken);
        if (book == null)
            return ResultViewModel.Error("Book not found");
        if(!book.IsBorrowed)
            return ResultViewModel.Error("Book is not borrowed");
        var loan = await context.Loans.SingleOrDefaultAsync(l=>l.IdBook == request.Id && !l.IsReturned, cancellationToken: cancellationToken);
        book.ChangeStatus();
        context.Books.Update(book);
        if (loan.IsLate())
        {
            loan.ReturnBook();
            context.Loans.Update(loan);
            await context.SaveChangesAsync(cancellationToken);
            return new ResultViewModel(true, "Loan is late");
        }
        loan.ReturnBook();
        context.Loans.Update(loan);
        await context.SaveChangesAsync(cancellationToken);
        return new ResultViewModel(true, "Loan is on time");
    }
}