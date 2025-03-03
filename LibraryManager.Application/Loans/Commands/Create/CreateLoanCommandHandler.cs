using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Loans.Commands.Create;

public class CreateLoanCommandHandler(IBookRepository bookRepository, ILoanRepository loanRepository)
: IRequestHandler<CreateLoanCommand, ResultViewModel<List<int>>>
{
    public async Task<ResultViewModel<List<int>>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var books = await bookRepository.ListBooksByIdAsync(request.IdBooks);
        if (books.Count == 0)
            return ResultViewModel<List<int>>.Error("No books found with the given id");
        if (books.Any(b => b.CurrentLoanId.HasValue))
        {
            return ResultViewModel<List<int>>.Error("At least one book is borrowed");
        }
        
        var loans = request.ToEntity();
        foreach (var loan in loans)
            await loanRepository.AddLoanAsync(loan);

        foreach (var book in books)
        {
            var loan = loans.SingleOrDefault(l => l.IdBook == book.Id);
            if (loan == null) continue;
            book.BorrowBook(loan);
            await bookRepository.UpdateBookAsync(book);
        }
        
        return ResultViewModel<List<int>>.Success(loans.Select(l => l.Id).ToList());
    }
}